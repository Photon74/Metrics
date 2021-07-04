using MediatR;
using MetricsAgent.Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers.Requests
{
    public class DateTimeRangeForHdd : IRequest<List<HddMetricDto>>
    {
        [FromRoute]
        public DateTimeOffset FromTime { get; set; }
        [FromRoute]
        public DateTimeOffset ToTime { get; set; }
    }
}
