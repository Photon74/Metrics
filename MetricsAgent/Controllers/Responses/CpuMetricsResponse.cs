using MetricsAgent.Controllers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Responses
{
    public class CpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
