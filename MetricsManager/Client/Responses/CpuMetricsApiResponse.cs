using MetricsManager.Client.Interfaces;
using MetricsManager.Client.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class CpuMetricsApiResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
