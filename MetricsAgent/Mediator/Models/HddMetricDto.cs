using System;

namespace MetricsAgent.Mediator.Models
{
    public class HddMetricDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
