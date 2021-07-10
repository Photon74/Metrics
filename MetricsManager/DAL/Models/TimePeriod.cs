using System;

namespace MetricsManager.DAL.Models
{
    public class TimePeriod
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
