using MetricsManager.Client.Requests;
using MetricsManager.Client.Responses;

namespace MetricsManager.Client
{
    interface IMetricsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request);
        RamMetricsResponse GetRamMetrics(RamMetricsRequest request);
        HddMetricsResponse GetHddMetrics(HddMetricsRequest request);
        DotNetMetricsResponse GetDotNetMetrics(DotNetMetricsRequest request);
        NetworkMetricsResponse GetNetworkMetrics(NetworkMetricsRequest request);
    }
}
