using MediatR;
using MetricsManager.Mediator.Responses;
using System;

namespace MetricsManager.Mediator.Requests
{
    public class AgentIdTimePeriodCpuRequest : IRequest<CpuMetricsResponse>
	{
		public int AgentId { get; set; }
		public DateTimeOffset FromTime { get; set; }
		public DateTimeOffset ToTime { get; set; }
	}
}
