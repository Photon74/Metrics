using MetricsAgent.Mediator.Models;
using System.Collections.Generic;

namespace MetricsAgent.Mediator.Responses
{
    public class HddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
