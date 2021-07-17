using System;

namespace MetricsManager.Client.Models
{
    public class CpuMetricDto
    {
        public int AgentId { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
