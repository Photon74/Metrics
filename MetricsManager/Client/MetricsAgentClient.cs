using MetricsManager.Client.Requests;
using MetricsManager.Client.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;

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

        public CpuMetricsApiResponse GetCpuMetrics(CpuMetricsApiRequest request)
        {
            var FromTime = request.FromTime.ToString("O");
            var ToTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/cpu/from/{FromTime}/to/{ToTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<CpuMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public DotNetMetricsApiResponse GetDotNetMetrics(DotNetMetricsRequest request)
        {
            var FromTime = request.FromTime.ToString("O");
            var ToTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/dotnet/from/{FromTime}/to/{ToTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var res = JsonSerializer.DeserializeAsync<DotNetMetricsApiResponse>(responseStream, options).Result;
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public HddMetricsApiResponse GetHddMetrics(HddMetricsRequest request)
        {
            var FromTime = request.FromTime.ToString("O");
            var ToTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/hdd/from/{FromTime}/to/{ToTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<HddMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public NetworkMetricsApiResponse GetNetworkMetrics(NetworkMetricsRequest request)
        {
            var FromTime = request.FromTime.ToString("O");
            var ToTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/network/from/{FromTime}/to/{ToTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<NetworkMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public RamMetricsApiResponse GetRamMetrics(RamMetricsRequest request)
        {
            var FromTime = request.FromTime.ToString("O");
            var ToTime = request.ToTime.ToString("O");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/ram/from/{FromTime}/to/{ToTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<RamMetricsApiResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
