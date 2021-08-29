using MetricsManager.Client.Models;
using System.Collections.Generic;

namespace MetricsManager.Mediator.Responses
{
    public class CpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
