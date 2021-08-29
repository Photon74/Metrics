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
    public class RamRequestHandler : IRequestHandler<DateTimeRangeForRam, RamMetricsResponse>
    {
        private readonly IRamMetricsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<RamRequestHandler> _logger;

        public RamRequestHandler(IRamMetricsRepository repository,
                                 IMapper mapper,
                                 ILogger<RamRequestHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<RamMetricsResponse> Handle(DateTimeRangeForRam request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Ram Metrics: from - {request.FromTime}, to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new RamMetricsResponse { Metrics = new List<RamMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
