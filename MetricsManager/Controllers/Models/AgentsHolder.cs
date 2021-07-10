using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers.Models
{
    public class AgentsHolder
    {
        public List<AgentInfo> agents;

        public AgentsHolder(List<AgentInfo> agents)
        {
            this.agents = agents;
        }
    }
}
