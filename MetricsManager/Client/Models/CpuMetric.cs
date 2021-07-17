using System;

namespace MetricsManager.Client.Models
{
    public class CpuMetric
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
