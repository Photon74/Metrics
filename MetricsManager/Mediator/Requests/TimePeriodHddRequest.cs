using MediatR;
using MetricsManager.Mediator.Responses;
using System;

namespace MetricsManager.Mediator.Requests
{
    public class TimePeriodHddRequest : IRequest<HddMetricsResponse>
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
