using MetricsView.Client.Models;
using System.Collections.Generic;

namespace MetricsView.Client.Responses
{
    public class Response
    {
        public List<MetricOUT> Metrics { get; set; }
    }
}