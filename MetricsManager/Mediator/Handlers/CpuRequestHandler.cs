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
    public class CpuRequestHandler : IRequestHandler<TimePeriodCpuRequest, CpuMetricsResponse>
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly ILogger<CpuRequestHandler> _logger;
        private readonly IMapper _mapper;

        public CpuRequestHandler(ICpuMetricsRepository repository,
                                 ILogger<CpuRequestHandler> logger,
                                 IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<CpuMetricsResponse> Handle(TimePeriodCpuRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Cpu Metrics: " +
                $"from all MetricsAgent " +
                $"from - {request.FromTime}, " +
                $"to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(_mapper.Map<TimePeriod>(request));

            var response = new CpuMetricsResponse { Metrics = new List<CpuMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
