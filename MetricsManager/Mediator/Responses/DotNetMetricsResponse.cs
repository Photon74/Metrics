using MetricsManager.Client.Models;
using System.Collections.Generic;

namespace MetricsManager.Mediator.Responses
{
    public class DotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}
