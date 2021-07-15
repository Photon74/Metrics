using MetricsAgent.Mediator.Models;
using System.Collections.Generic;

namespace MetricsAgent.Mediator.Responses
{
    public class CpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
