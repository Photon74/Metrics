using MetricsAgent.Controllers.Models;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class DotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}