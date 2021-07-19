using MediatR;
using MetricsManager.Mediator.Responses;
using System;

namespace MetricsManager.Mediator.Requests
{
    public class AgentIdTimePeriodCpuRequest : IRequest<CpuMetricsResponse>
	{
		public int agentId { get; set; }
		public DateTimeOffset FromTime { get; set; }
		public DateTimeOffset ToTime { get; set; }
	}
}
