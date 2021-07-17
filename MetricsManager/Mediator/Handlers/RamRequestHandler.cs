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
    public class RamRequestHandler : IRequestHandler<TimePeriodRamRequest, RamMetricsResponse>
    {
        private readonly IRamMetricsRepository _repository;
        private readonly ILogger<RamRequestHandler> _logger;
        private readonly IMapper _mapper;

        public RamRequestHandler(IRamMetricsRepository repository,
                                 ILogger<RamRequestHandler> logger,
                                 IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<RamMetricsResponse> Handle(TimePeriodRamRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Ram Metrics: " +
                $"from all MetricsAgent " +
                $"from - {request.FromTime}, " +
                $"to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(_mapper.Map<TimePeriod>(request));

            var response = new RamMetricsResponse { Metrics = new List<RamMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
