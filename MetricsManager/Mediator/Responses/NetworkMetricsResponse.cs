using MetricsManager.Client.Models;
using System.Collections.Generic;

namespace MetricsManager.Mediator.Responses
{
    public class NetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
