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
    public class AgentIdCpuRequestHandler : IRequestHandler<AgentIdTimePeriodCpuRequest, CpuMetricsResponse>
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<AgentIdCpuRequestHandler> _logger;
        private readonly IMapper _mapper;

        public AgentIdCpuRequestHandler(ICpuMetricsRepository repository,
                                        ILogger<AgentIdCpuRequestHandler> logger,
                                        IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<CpuMetricsResponse> Handle(AgentIdTimePeriodCpuRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Cpu Metrics: " +
                $"from MetricsAgent - {request.agentId} " +
                $"from - {request.FromTime}, " +
                $"to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriodFromAgent(_mapper.Map<AgentIdTimePeriod>(request));
            var response = new CpuMetricsResponse { Metrics = new List<CpuMetricDto>() };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
