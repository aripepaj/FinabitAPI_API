namespace FinabitAPI.Utilis
{
    public static class SqlConn
    {
        public static string Build(string server, string database)
        {
            var s = server?.Trim() ?? "";
            if (!s.StartsWith("tcp:", StringComparison.OrdinalIgnoreCase) &&
                !s.StartsWith("np:", StringComparison.OrdinalIgnoreCase))
            {
                s = "tcp:" + s;
            }

            return $"Server={s};Database={database};User ID=Fina;Password=Fina-10;" +
                   "Encrypt=True;TrustServerCertificate=True;" +
                   "ConnectRetryCount=3;ConnectRetryInterval=2;" +
                   "Pooling=True;Min Pool Size=1;Connect Timeout=15";
        }
    }
}
