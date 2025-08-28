using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;
using FinabitAPI.Utilis;

public sealed class DatabaseBootstrapper : IHostedService
{
    private readonly ILogger<DatabaseBootstrapper> _log;
    private readonly IServiceScopeFactory _scopeFactory;

    public DatabaseBootstrapper(ILogger<DatabaseBootstrapper> log, IServiceScopeFactory scopeFactory)
    {
        _log = log;
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync(CancellationToken ct)
    {
        try
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

            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DBAccess>();

            await using var cn = db.GetConnection();  
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

            _log.LogInformation("SQL bootstrap completed.");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Database bootstrap failed; API will continue to run.");
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
