using MetricsView.Client.Responses;
using NLog;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Threading;
using System.Windows;

namespace MetricsView.Client
{
    public class MetricsManagerClient : IMetricsManagerClient
    {
        private readonly HttpClient _httpClient;
        private static Logger _logger;

        public MetricsManagerClient()
        {
            _httpClient = new HttpClient();
            _logger = LogManager.GetCurrentClassLogger();
        }

        public Response GetCpuMetrics()
        {
            var FromTime = DateTimeOffset.Now.AddMinutes(-1).ToString("O");
            var ToTime = DateTimeOffset.Now.ToString("O");

            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"http://localhost:5002/api/metrics/cpu/cluster/from/{FromTime}/to/{ToTime}");
            // http://localhost:5002/api/metrics/ram/cluster/from/2021-07-16/to/2021-07-26
            // http://localhost:5002/cluster/api/metrics/cpu/from/{FromTime}/to/{ToTime}

            try
            {
                var responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var res = JsonSerializer.DeserializeAsync<Response>(
                    responseStream,
                    options
                    ).Result;
                return res;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return null;
        }
        public void Start()
        {
            var timer = new DispatcherTimer();

            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            GetCpuMetrics();
        }

        //public DotNetMetricsApiResponse GetDotNetMetrics(DotNetMetricsRequest request)
        //{
        //    var FromTime = request.FromTime.ToString("O");
        //    var ToTime = request.ToTime.ToString("O");

        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
        //        $"{request.AgentUrl}api/metrics/dotnet/from/{FromTime}/to/{ToTime}");

        //    try
        //    {
        //        HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
        //        using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        };
        //        var res = JsonSerializer.DeserializeAsync<DotNetMetricsApiResponse>(responseStream, options).Result;
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}

        //public HddMetricsApiResponse GetHddMetrics(HddMetricsRequest request)
        //{
        //    var FromTime = request.FromTime.ToString("O");
        //    var ToTime = request.ToTime.ToString("O");

        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
        //        $"{request.AgentUrl}api/metrics/hdd/from/{FromTime}/to/{ToTime}");

        //    try
        //    {
        //        HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
        //        using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        };
        //        return JsonSerializer.DeserializeAsync<HddMetricsApiResponse>(responseStream, options).Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}

        //public NetworkMetricsApiResponse GetNetworkMetrics(NetworkMetricsRequest request)
        //{
        //    var FromTime = request.FromTime.ToString("O");
        //    var ToTime = request.ToTime.ToString("O");

        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
        //        $"{request.AgentUrl}api/metrics/network/from/{FromTime}/to/{ToTime}");

        //    try
        //    {
        //        HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
        //        using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        };
        //        return JsonSerializer.DeserializeAsync<NetworkMetricsApiResponse>(responseStream, options).Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}

        //public RamMetricsApiResponse GetRamMetrics(RamMetricsRequest request)
        //{
        //    var FromTime = request.FromTime.ToString("O");
        //    var ToTime = request.ToTime.ToString("O");

        //    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
        //        $"{request.AgentUrl}api/metrics/ram/from/{FromTime}/to/{ToTime}");

        //    try
        //    {
        //        HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
        //        using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
        //        var options = new JsonSerializerOptions
        //        {
        //            PropertyNameCaseInsensitive = true
        //        };
        //        return JsonSerializer.DeserializeAsync<RamMetricsApiResponse>(responseStream, options).Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //    }
        //    return null;
        //}
    }
}
