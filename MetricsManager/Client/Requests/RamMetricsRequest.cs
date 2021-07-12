using System;

namespace MetricsManager.Client.Requests
{
    public class RamMetricsRequest
    {
        public Uri AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
