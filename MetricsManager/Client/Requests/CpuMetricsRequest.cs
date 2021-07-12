using System;

namespace MetricsManager.Client.Requests
{
    public class CpuMetricsRequest
    {
        public Uri AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
