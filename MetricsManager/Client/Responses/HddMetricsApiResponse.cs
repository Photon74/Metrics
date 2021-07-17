using MetricsManager.Client.Models;
using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class HddMetricsApiResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
