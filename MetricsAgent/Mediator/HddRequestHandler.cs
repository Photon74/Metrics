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
    public class HddRequestHandler : IRequestHandler<DateTimeRangeForHdd, HddMetricsResponse>
    {
        private readonly IHddMetricsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<HddRequestHandler> _logger;

        public HddRequestHandler(IHddMetricsRepository repository, IMapper mapper, ILogger<HddRequestHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<HddMetricsResponse> Handle(DateTimeRangeForHdd request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Hdd Metrics: from - {request.FromTime}, to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new HddMetricsResponse { Metrics = new List<HddMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
