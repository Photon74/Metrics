using AutoMapper;
using MediatR;
using MetricsManager.Client.Models;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using MetricsManager.Mediator.Requests;
using MetricsManager.Mediator.Responses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsManager.Mediator.Handlers
{
    public class AgentIdNetworkRequestHandler : IRequestHandler<AgentIdTimePeriodNetworkRequest, NetworkMetricsResponse>
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly ILogger<AgentIdNetworkRequestHandler> _logger;
        private readonly IMapper _mapper;

        public AgentIdNetworkRequestHandler(INetworkMetricsRepository repository,
                                        ILogger<AgentIdNetworkRequestHandler> logger,
                                        IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<NetworkMetricsResponse> Handle(AgentIdTimePeriodNetworkRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Network Metrics: " +
                $"from MetricsAgent - {request.AgentId} " +
                $"from - {request.FromTime}, " +
                $"to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriodFromAgent(_mapper.Map<AgentIdTimePeriod>(request));
            var response = new NetworkMetricsResponse { Metrics = new List<NetworkMetricDto>() };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
