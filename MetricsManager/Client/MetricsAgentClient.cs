using MetricsManager.Client.Requests;
using MetricsManager.Client.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<CpuMetricsApiResponse> GetCpuMetrics(CpuMetricsApiRequest request)
        {
            var fromTime = request.FromTime.ToString("O");
            var toTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/cpu/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                await using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var value = JsonSerializer.DeserializeAsync<CpuMetricsApiResponse>(responseStream, options);
                if (value.IsCompleted)
                {
                    return value.Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<DotNetMetricsApiResponse> GetDotNetMetrics(DotNetMetricsRequest request)
        {
            var fromTime = request.FromTime.ToString("O");
            var toTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/dotnet/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                await using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var value = JsonSerializer.DeserializeAsync<DotNetMetricsApiResponse>(responseStream, options);
                if (value.IsCompleted)
                {
                    return value.Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<HddMetricsApiResponse> GetHddMetrics(HddMetricsRequest request)
        {
            var fromTime = request.FromTime.ToString("O");
            var toTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/hdd/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                await using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var value = JsonSerializer.DeserializeAsync<HddMetricsApiResponse>(responseStream, options);
                if (value.IsCompleted)
                {
                    return value.Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<NetworkMetricsApiResponse> GetNetworkMetrics(NetworkMetricsRequest request)
        {
            var fromTime = request.FromTime.ToString("O");
            var toTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/network/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                await using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var value = JsonSerializer.DeserializeAsync<NetworkMetricsApiResponse>(responseStream, options);
                if (value.IsCompleted)
                {
                    return value.Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<RamMetricsApiResponse> GetRamMetrics(RamMetricsRequest request)
        {
            var fromTime = request.FromTime.ToString("O");
            var toTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/ram/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                await using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var value = JsonSerializer.DeserializeAsync<RamMetricsApiResponse>(responseStream, options);
                if (value.IsCompleted)
                {
                    return value.Result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
