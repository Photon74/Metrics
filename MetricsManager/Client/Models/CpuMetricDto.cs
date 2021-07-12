using MetricsManager.Client.Interfaces;
using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Models
{
    public class CpuMetricDto : IMetricsResponse<CpuMetricDto>
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
        public IList<CpuMetricDto> Metrics { get; set; }
    }
}
