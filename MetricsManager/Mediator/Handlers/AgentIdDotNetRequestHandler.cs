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
    public class AgentIdDotNetRequestHandler : IRequestHandler<AgentIdTimePeriodDotNetRequest, DotNetMetricsResponse>
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly ILogger<AgentIdDotNetRequestHandler> _logger;
        private readonly IMapper _mapper;

        public AgentIdDotNetRequestHandler(IDotNetMetricsRepository repository,
                                        ILogger<AgentIdDotNetRequestHandler> logger,
                                        IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<DotNetMetricsResponse> Handle(AgentIdTimePeriodDotNetRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting DotNet Metrics: " +
                $"from MetricsAgent - {request.agentId} " +
                $"from - {request.FromTime}, " +
                $"to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriodFromAgent(_mapper.Map<AgentIdTimePeriod>(request));
            var response = new DotNetMetricsResponse { Metrics = new List<DotNetMetricDto>() };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
