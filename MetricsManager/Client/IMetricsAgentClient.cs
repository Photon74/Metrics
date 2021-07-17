using MetricsManager.Client.Requests;
using MetricsManager.Client.Responses;
using System;
using System.Collections.Generic;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsApiResponse GetCpuMetrics(CpuMetricsApiRequest request);
        RamMetricsResponse GetRamMetrics(RamMetricsRequest request);
        HddMetricsResponse GetHddMetrics(HddMetricsRequest request);
        DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest request);
        NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request);
    }
}
