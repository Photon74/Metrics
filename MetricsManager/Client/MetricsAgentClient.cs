﻿using MetricsManager.Client.Requests;
using MetricsManager.Client.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public IEnumerable<CpuMetricsResponse> GetCpuMetrics(CpuMetricsRequest request)
        {
            var fromTime = request.FromTime;
            var toTime = request.ToTime;

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}api/metrics/cpu/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var res = JsonSerializer.DeserializeAsync<IEnumerable<CpuMetricsResponse>>(responseStream, options).Result;
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeSeconds();
            var toTime = request.ToTime.ToUnixTimeSeconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}/api/metrics/dotnet/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<DotNetMetricsResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public HddMetricsResponse GetHddMetrics(HddMetricsRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeSeconds();
            var toTime = request.ToTime.ToUnixTimeSeconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}/api/metrics/hdd/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<HddMetricsResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeSeconds();
            var toTime = request.ToTime.ToUnixTimeSeconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}/api/metrics/network/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<NetworkMetricsResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public RamMetricsResponse GetRamMetrics(RamMetricsRequest request)
        {
            var fromTime = request.FromTime.ToUnixTimeSeconds();
            var toTime = request.ToTime.ToUnixTimeSeconds();

            var httpRequest = new HttpRequestMessage(HttpMethod.Get,
                $"{request.AgentUrl}/api/metrics/ram/from/{fromTime}/to/{toTime}");

            try
            {
                HttpResponseMessage responseMessage = _httpClient.SendAsync(httpRequest).Result;
                using var responseStream = responseMessage.Content.ReadAsStreamAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.DeserializeAsync<RamMetricsResponse>(responseStream, options).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
