using AutoMapper;
using MediatR;
using MetricsAgent.Controllers.Models;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsAgent.Mediator
{
    public class CpuRequestHandler : IRequestHandler<DateTimeRangeForCpu, List<CpuMetricDto>>
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

        public Task<List<CpuMetricDto>> Handle(DateTimeRangeForCpu request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Cpu Metrics: from - {request.FromTime}, to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new List<CpuMetricDto>();

            foreach (var metric in metrics)
            {
                response.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
