using MetricsManager.Client.Interfaces;
using MetricsManager.Client.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class CpuMetricsApiResponse
    {
        public List<CpuMetric> Metrics { get; set; }
    }
}
