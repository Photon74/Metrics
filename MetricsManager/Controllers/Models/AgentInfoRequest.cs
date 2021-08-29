using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers.Models
{
    public class AgentInfoRequest
    {
        public string Host { get; set; }
        public bool Active { get; set; }
    }
}
