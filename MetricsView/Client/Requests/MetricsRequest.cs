using System;

namespace MetricsView.Client.Requests
{
    public class MetricsRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}