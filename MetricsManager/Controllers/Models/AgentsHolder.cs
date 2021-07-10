using System.Collections.Generic;

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
