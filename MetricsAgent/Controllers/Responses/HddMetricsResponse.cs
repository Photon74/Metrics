using MetricsAgent.Controllers.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Models
{
    public class HddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
