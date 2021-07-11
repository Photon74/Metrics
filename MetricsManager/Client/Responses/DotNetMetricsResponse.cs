using MetricsManager.Client.Models;
using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class DotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}
