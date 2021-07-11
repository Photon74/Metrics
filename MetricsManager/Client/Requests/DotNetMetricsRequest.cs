using System;

namespace MetricsManager.Client.Requests
{
    public class DotNetMetricsRequest
    {
        public string AgentUrl { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
