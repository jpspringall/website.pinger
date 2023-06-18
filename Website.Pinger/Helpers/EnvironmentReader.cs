namespace Website.Pinger.Helpers
{
    public static class EnvironmentReader
    {
        public static long PingsSinceStart
        {
            get
            {
                var pingsSinceStart = Environment.GetEnvironmentVariable("PINGS_SINCE_START");
                return string.IsNullOrWhiteSpace(pingsSinceStart) ? 0 : System.Convert.ToInt32(pingsSinceStart);
            }
            set =>
                Environment.SetEnvironmentVariable("PINGS_SINCE_START",
                    (value > long.MaxValue - 10 ? 0 : value).ToString());
        }

        public static int PingerIntervalInMinutes
        {
            get
            {
                var pingerIntervalInMinutes = Environment.GetEnvironmentVariable("PINGER_INTERVAL_IN_MINUTES");
                return string.IsNullOrWhiteSpace(pingerIntervalInMinutes) ? 0 : System.Convert.ToInt32(pingerIntervalInMinutes);
            }
        }

        public static string[] PingerUrls
        {
            get
            {
                var pingerUrls = Environment.GetEnvironmentVariable("PINGER_URLS_CSV");
                return string.IsNullOrWhiteSpace(pingerUrls) ? Array.Empty<string>() : pingerUrls.Split(',');
            }
        }

        public static string StorageFile => System.IO.Path.Combine(System.IO.Path.GetTempPath(), "website.pinger.storage.json");
    }
}
