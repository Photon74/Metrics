using MediatR;
using MetricsAgent.Mediator.Models;
using MetricsAgent.Mediator.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Mediator.Requests
{
    public class DateTimeRangeForNetwork : IRequest<NetworkMetricsResponse>
    {
        [FromRoute]
        public DateTimeOffset FromTime { get; set; }
        [FromRoute]
        public DateTimeOffset ToTime { get; set; }
    }
}
