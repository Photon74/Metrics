using MetricsManager.Client.Requests;
using MetricsManager.Client.Responses;
using System;
using System.Collections.Generic;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        CpuMetricsApiResponse GetCpuMetrics(CpuMetricsApiRequest request);
        RamMetricsApiResponse GetRamMetrics(RamMetricsRequest request);
        HddMetricsApiResponse GetHddMetrics(HddMetricsRequest request);
        DotNetMetricsApiResponse GetDotNetMetrics(DotNetMetricsRequest request);
        NetworkMetricsApiResponse GetNetworkMetrics(NetworkMetricsRequest request);
    }
}
