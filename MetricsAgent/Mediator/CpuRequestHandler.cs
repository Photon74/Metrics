using AutoMapper;
using MediatR;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.Mediator.Models;
using MetricsAgent.Mediator.Requests;
using MetricsAgent.Mediator.Responses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsAgent.Mediator
{
    public class CpuRequestHandler : IRequestHandler<DateTimeRangeForCpu, CpuMetricsResponse>
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CpuRequestHandler> _logger;

        public CpuRequestHandler(ICpuMetricsRepository repository,
                                 IMapper mapper,
                                 ILogger<CpuRequestHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<CpuMetricsResponse> Handle(DateTimeRangeForCpu request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Cpu Metrics: from - {request.FromTime}, to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new CpuMetricsResponse { Metrics = new List<CpuMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
