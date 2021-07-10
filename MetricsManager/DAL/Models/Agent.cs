using System;

namespace MetricsManager.DAL.Models
{
    public class Agent
    {
        public int AgentId { get; set; }
        public Uri AgentUrl { get; set; }
        public bool Enabled { get; set; }
    }
}
