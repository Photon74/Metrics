using MediatR;
using MetricsAgent.Mediator.Models;
using MetricsAgent.Mediator.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Mediator.Requests
{
    public class DateTimeRangeForDotNet : IRequest<DotNetMetricsResponse>
    {
        [FromRoute]
        public DateTimeOffset FromTime { get; set; }
        [FromRoute]
        public DateTimeOffset ToTime { get; set; }
    }
}
