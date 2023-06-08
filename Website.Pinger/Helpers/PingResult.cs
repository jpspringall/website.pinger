namespace Website.Pinger.Helpers
{
    public class PingResult
    {
        public string Url { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
        public DateTime NextPing { get; set; }
    }
}
