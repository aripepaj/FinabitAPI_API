using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Utilis;

public sealed class DatabaseBootstrapper : IHostedService
{
    private readonly ILogger<DatabaseBootstrapper> _log;
    private readonly IServiceScopeFactory _scopeFactory;

    private static readonly Regex GoRegex = new(@"^\s*GO\s*;$|^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex LeadingNumber = new(@"^(\d+)", RegexOptions.Compiled);

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

            // read all .sql files and extract numeric versions
            var candidates = Directory.GetFiles(sqlDir, "*.sql", SearchOption.TopDirectoryOnly)
                .Select(p => new { Path = p, Version = GetVersionFromFileName(Path.GetFileName(p)) })
                .Where(x => x.Version >= 0)
                .OrderBy(x => x.Version)
                .ThenBy(x => x.Path, StringComparer.OrdinalIgnoreCase)
                .ToArray();

            if (candidates.Length == 0)
            {
                _log.LogInformation("No versioned .sql files in {Dir}.", sqlDir);
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DBAccess>();

            await using var cn = db.GetConnection();
            await cn.OpenAsync(ct);

            await EnsureExclusiveLock(cn, ct);

            await EnsureMigrationsTable(cn, ct);

            int current = await GetCurrentVersion(cn, ct);

            var toRun = candidates.Where(c => c.Version > current).ToList();
            if (toRun.Count == 0)
            {
                _log.LogInformation("Database bootstrap: up to date at version {Version}.", current);
                return;
            }

            _log.LogInformation("Applying {Count} SQL script(s), from version {From} to {To}.",
                toRun.Count, toRun.First().Version, toRun.Last().Version);

            foreach (var script in toRun)
            {
                var text = await File.ReadAllTextAsync(script.Path, ct);

                await using var tx = await cn.BeginTransactionAsync(ct);
                try
                {
                    foreach (var batch in SplitBatches(text))
                    {
                        if (string.IsNullOrWhiteSpace(batch)) continue;
                        await using var cmd = new SqlCommand(batch, cn, (SqlTransaction)tx)
                        {
                            CommandTimeout = 60
                        };
                        await cmd.ExecuteNonQueryAsync(ct);
                    }

                    await using (var cmd = new SqlCommand(
                        "INSERT INTO dbo.SchemaMigrations(Version, FileName) VALUES (@v, @f);", cn, (SqlTransaction)tx))
                    {
                        cmd.Parameters.AddWithValue("@v", script.Version);
                        cmd.Parameters.AddWithValue("@f", Path.GetFileName(script.Path));
                        await cmd.ExecuteNonQueryAsync(ct);
                    }

                    await tx.CommitAsync(ct);
                    _log.LogInformation("Applied {File} (version {Version}).",
                        Path.GetFileName(script.Path), script.Version);
                }
                catch (Exception ex)
                {
                    await tx.RollbackAsync(ct);
                    _log.LogError(ex, "Failed applying {File} (version {Version}). Stopping.",
                        Path.GetFileName(script.Path), script.Version);
                    throw; // stop on first failure
                }
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
        int last = 0;
        foreach (Match m in GoRegex.Matches(sql))
        {
            var len = m.Index - last;
            if (len > 0) yield return sql.Substring(last, len);
            last = m.Index + m.Length;
        }
        if (last < sql.Length) yield return sql.Substring(last);
    }

    private static int GetVersionFromFileName(string fileName)
    {
        var m = LeadingNumber.Match(fileName ?? "");
        return m.Success ? int.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture) : -1;
    }

    private static async Task EnsureExclusiveLock(SqlConnection cn, CancellationToken ct)
    {
        const string sql = @"
DECLARE @res int;
EXEC @res = sys.sp_getapplock 
     @Resource = N'Finabit.DbBootstrapper',
     @LockMode = N'Exclusive',
     @LockOwner = N'Session',
     @LockTimeout = 15000; 
SELECT @res;";
        await using var cmd = new SqlCommand(sql, cn);
        var obj = await cmd.ExecuteScalarAsync(ct);
        var res = Convert.ToInt32(obj, CultureInfo.InvariantCulture);

        if (res < 0)
            throw new InvalidOperationException($"Failed to acquire bootstrapper lock (sp_getapplock result {res}).");
    }

    private static async Task EnsureMigrationsTable(SqlConnection cn, CancellationToken ct)
    {
        const string sql = @"
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID(N'dbo.SchemaMigrations'))
BEGIN
    CREATE TABLE dbo.SchemaMigrations 
    (
        Version   int           NOT NULL PRIMARY KEY,
        FileName  nvarchar(260) NOT NULL,
        AppliedAt datetime2     NOT NULL CONSTRAINT DF_SchemaMigrations_AppliedAt DEFAULT (sysutcdatetime())
    );
END";
        await using var cmd = new SqlCommand(sql, cn);
        await cmd.ExecuteNonQueryAsync(ct);
    }

    private static async Task<int> GetCurrentVersion(SqlConnection cn, CancellationToken ct)
    {
        await using var cmd = new SqlCommand("SELECT ISNULL(MAX(Version), 0) FROM dbo.SchemaMigrations;", cn);
        var result = await cmd.ExecuteScalarAsync(ct);
        return result is int i ? i : Convert.ToInt32(result, CultureInfo.InvariantCulture);
    }
}
