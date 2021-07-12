using MetricsManager.Client.Interfaces;
using MetricsManager.Client.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.Client.Responses
{
    public class CpuMetricsResponse
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
        public IList<CpuMetricsResponse> Metrics { get; set; }

        public IEnumerator<CpuMetricsResponse> GetEnumerator()
        {
            return Metrics.GetEnumerator();
        }
    }
}
