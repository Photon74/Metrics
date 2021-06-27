using MetricsAgent.Controllers.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Models
{
    public class RamMetricsResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }
}
