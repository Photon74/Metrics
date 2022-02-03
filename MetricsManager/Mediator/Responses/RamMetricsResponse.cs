using MetricsManager.Client.Models;
using System.Collections.Generic;

namespace MetricsManager.Mediator.Responses
{
    public class RamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
