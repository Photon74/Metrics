using MediatR;
using MetricsAgent.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Requests
{
    public class DateTimeRangeForCpu : IRequest<List<CpuMetricDto>>
    {
        [FromRoute]
        public DateTimeOffset FromTime { get; set; }
        [FromRoute]
        public DateTimeOffset ToTime { get; set; }
    }
}
