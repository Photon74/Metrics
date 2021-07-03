using System.Collections.Generic;

namespace MetricsAgent.Controllers.Models
{
    public class NetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
