using MediatR;
using MetricsAgent.Mediator.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MetricsAgent.Mediator.Requests
{
    public class DateTimeRangeForCpu : IRequest<CpuMetricsResponse>
    {
        [FromRoute]
        public DateTimeOffset FromTime { get; set; }
        [FromRoute]
        public DateTimeOffset ToTime { get; set; }
    }
}
