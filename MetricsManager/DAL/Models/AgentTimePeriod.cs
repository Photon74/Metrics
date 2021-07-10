using System;

namespace MetricsManager.DAL.Models
{
    public class AgentTimePeriod
    {
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
