using MetricsAgent.Mediator.Models;
using System.Collections.Generic;

namespace MetricsAgent.Mediator.Responses
{
    public class DotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}