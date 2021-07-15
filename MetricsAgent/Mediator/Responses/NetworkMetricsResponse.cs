using MetricsAgent.Mediator.Models;
using System.Collections.Generic;

namespace MetricsAgent.Mediator.Responses
{
    public class NetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
