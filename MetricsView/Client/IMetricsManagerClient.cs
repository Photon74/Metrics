using MetricsView.Client.Requests;
using MetricsView.Client.Responses;

namespace MetricsView.Client
{
    public interface IMetricsManagerClient
    {
        Response GetCpuMetrics();
        //RamMetricsApiResponse GetRamMetrics(RamMetricsRequest request);
        //HddMetricsApiResponse GetHddMetrics(HddMetricsRequest request);
        //DotNetMetricsApiResponse GetDotNetMetrics(DotNetMetricsRequest request);
        //NetworkMetricsApiResponse GetNetworkMetrics(NetworkMetricsRequest request);
    }
}
