using MetricsManager.Client.Models;
using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class RamMetricsApiResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
