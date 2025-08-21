using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using FinabitAPI.Utilis;

public sealed class DatabaseBootstrapper : IHostedService
{
    private readonly ILogger<DatabaseBootstrapper> _log;
    private readonly DBAccess _db;

    public DatabaseBootstrapper(ILogger<DatabaseBootstrapper> log, DBAccess db)
    {
        _log = log;
        _db = db;
    }

    public async Task StartAsync(CancellationToken ct)
    {
        var sqlDir = Path.Combine(AppContext.BaseDirectory, "sql");
        if (!Directory.Exists(sqlDir))
        {
            _log.LogInformation("No SQL bootstrap directory at {Dir}; skipping.", sqlDir);
            return;
        }

        var scripts = Directory.GetFiles(sqlDir, "*.sql", SearchOption.TopDirectoryOnly)
                               .OrderBy(p => p, StringComparer.OrdinalIgnoreCase)
                               .ToArray();
        if (scripts.Length == 0)
        {
            _log.LogInformation("No .sql files in {Dir}.", sqlDir);
            return;
        }

        await using var cn = _db.GetConnection();
        await cn.OpenAsync(ct);

        foreach (var path in scripts)
        {
            var text = await File.ReadAllTextAsync(path, ct);
            foreach (var batch in SplitBatches(text))
            {
                if (string.IsNullOrWhiteSpace(batch)) continue;
                await using var cmd = new SqlCommand(batch, cn) { CommandTimeout = 60 };
                await cmd.ExecuteNonQueryAsync(ct);
            }
            _log.LogInformation("Executed SQL bootstrap: {File}", Path.GetFileName(path));
        }
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;

    private static IEnumerable<string> SplitBatches(string sql)
    {
        var re = new Regex(@"^\s*GO\s*;$|^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        int last = 0;
        foreach (Match m in re.Matches(sql))
        {
            var len = m.Index - last;
            if (len > 0) yield return sql.Substring(last, len);
            last = m.Index + m.Length;
        }
        if (last < sql.Length) yield return sql.Substring(last);
    }
}
