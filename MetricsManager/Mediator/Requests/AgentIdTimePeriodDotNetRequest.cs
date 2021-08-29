using MediatR;
using MetricsManager.Mediator.Responses;
using System;

namespace MetricsManager.Mediator.Requests
{
    public class AgentIdTimePeriodDotNetRequest : IRequest<DotNetMetricsResponse>
	{
		public int AgentId { get; set; }
		public DateTimeOffset FromTime { get; set; }
		public DateTimeOffset ToTime { get; set; }
	}
}
