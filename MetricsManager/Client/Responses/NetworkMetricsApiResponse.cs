using MetricsManager.Client.Models;
using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class NetworkMetricsApiResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
