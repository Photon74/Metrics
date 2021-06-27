using MetricsAgent.Controllers.Responses;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Models
{
    public class HddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
