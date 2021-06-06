using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class AgentInfo
    {
        public int agentId { get; set; }
        public Uri AgentAddress { get; set; }

        public AgentInfo(int agentId, Uri agentAddress)
        {
            this.agentId = agentId;
            AgentAddress = agentAddress;
        }
    }
}
