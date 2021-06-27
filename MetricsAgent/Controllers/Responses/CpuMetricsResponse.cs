using MetricsAgent.Controllers.Models;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Responses
{
    public class CpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
