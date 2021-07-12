using System;

namespace MetricsAgent.Controllers.Models
{
    public class NetworkMetricDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
