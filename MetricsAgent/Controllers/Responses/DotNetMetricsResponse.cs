﻿using MetricsAgent.Controllers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Responses
{
    public class DotNetMetricsResponse
    {
        public List<DotNetMetricDto> Metrics { get; set; }
    }
}