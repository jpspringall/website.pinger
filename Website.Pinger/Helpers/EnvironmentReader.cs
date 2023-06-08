namespace Website.Pinger.Helpers
{
    public static class EnvironmentReader
    {
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
