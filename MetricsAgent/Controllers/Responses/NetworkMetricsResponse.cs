using MetricsAgent.Controllers.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Models
{
    public class NetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
