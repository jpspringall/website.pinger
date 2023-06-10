using Website.Pinger.Helpers;

namespace Website.Pinger.Workers
{
    public class Pinger : BackgroundService
    {
        private readonly ILogger<Pinger> _logger;
        private readonly HttpClient _httpClient;
        private readonly int _pingerIntervalInMinutes;
        private readonly string[] _pingerUrls;

        public Pinger(ILogger<Pinger> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _pingerIntervalInMinutes = EnvironmentReader.PingerIntervalInMinutes;
            _pingerUrls = EnvironmentReader.PingerUrls;

            _logger.LogInformation("Storage file: {storageFile}", EnvironmentReader.StorageFile);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Starting Pinger With Interval {interval} on Urls {urls} ", _pingerIntervalInMinutes, _pingerUrls);

                if (_pingerIntervalInMinutes > 0 && _pingerUrls.Any())
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        await PingUrls();
                        await Task.Delay(TimeSpan.FromMinutes(_pingerIntervalInMinutes), stoppingToken);
                    }
                }

                _logger.LogInformation("Stopping: {status}", stoppingToken.IsCancellationRequested);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Stopping due to error");

            }
        }

        private async Task PingUrls()
        {
            var results = new List<PingResult>();
            try
            {
                foreach (var url in _pingerUrls)
                {
                    var currentTime = System.DateTime.Now;
                    results.Add(new PingResult
                    {
                        Url = url,
                        Status = (await _httpClient.GetAsync($"{url}?q={DateTime.Now.ToFileTime()}")).StatusCode.ToString(),
                        Time = currentTime,
                        TimeTaken = DateTime.Now - currentTime,
                        NextPing = currentTime.AddMinutes(_pingerIntervalInMinutes)
                    });
                }

            }
            catch (Exception ex)
            {
                var currentTime = System.DateTime.Now;
                _logger.LogError(ex, "Error ping urls");
                results.Add(new PingResult { Url = ex.ToString(), Status = "Error", TimeTaken = TimeSpan.FromTicks(0), Time = currentTime, NextPing = currentTime.AddMinutes(_pingerIntervalInMinutes) });
            }

            await System.IO.File.WriteAllTextAsync(EnvironmentReader.StorageFile, Newtonsoft.Json.JsonConvert.SerializeObject(results));
        }
    }
}