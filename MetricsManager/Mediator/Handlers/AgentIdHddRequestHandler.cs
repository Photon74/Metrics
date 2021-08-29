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
    public class AgentIdHddRequestHandler : IRequestHandler<AgentIdTimePeriodHddRequest, HddMetricsResponse>
    {
        private readonly IHddMetricsRepository _repository;
        private readonly ILogger<AgentIdHddRequestHandler> _logger;
        private readonly IMapper _mapper;

        public AgentIdHddRequestHandler(IHddMetricsRepository repository,
                                        ILogger<AgentIdHddRequestHandler> logger,
                                        IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<HddMetricsResponse> Handle(AgentIdTimePeriodHddRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Hdd Metrics: " +
                $"from MetricsAgent - {request.AgentId} " +
                $"from - {request.FromTime}, " +
                $"to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriodFromAgent(_mapper.Map<AgentIdTimePeriod>(request));
            var response = new HddMetricsResponse { Metrics = new List<HddMetricDto>() };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
