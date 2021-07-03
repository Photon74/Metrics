using System.Collections.Generic;

namespace MetricsAgent.Controllers.Models
{
    public class RamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
