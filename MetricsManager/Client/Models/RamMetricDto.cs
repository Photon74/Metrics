using System;

namespace MetricsManager.Client.Models
{
    public class RamMetricDto
    {
        public int AgentId { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
