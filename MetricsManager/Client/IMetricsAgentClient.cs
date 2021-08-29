using MetricsManager.Client.Requests;
using MetricsManager.Client.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        Task<CpuMetricsApiResponse> GetCpuMetrics(CpuMetricsApiRequest request);
        Task<RamMetricsApiResponse> GetRamMetrics(RamMetricsRequest request);
        Task<HddMetricsApiResponse> GetHddMetrics(HddMetricsRequest request);
        Task<DotNetMetricsApiResponse> GetDotNetMetrics(DotNetMetricsRequest request);
        Task<NetworkMetricsApiResponse> GetNetworkMetrics(NetworkMetricsRequest request);
    }
}
