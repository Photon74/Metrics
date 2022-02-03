using MediatR;
using MetricsManager.Mediator.Responses;
using System;

namespace MetricsManager.Mediator.Requests
{
    public class AgentIdTimePeriodNetworkRequest : IRequest<NetworkMetricsResponse>
	{
		public int agentId
        {
            get; set;
        }
        public DateTimeOffset FromTime
        {
            get; set;
        }
        public DateTimeOffset ToTime
        {
            get; set;
        }
	}
}
