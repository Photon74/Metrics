﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers.Responses
{
    public class ByTimePeriodCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}
