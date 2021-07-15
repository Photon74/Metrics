using MetricsAgent.Mediator.Models;
using System.Collections.Generic;

namespace MetricsAgent.Mediator.Responses
{
    public class RamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
