using MetricsManager.Client.Models;
using System.Collections.Generic;

namespace MetricsManager.Mediator.Responses
{
    public class HddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
