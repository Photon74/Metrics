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
    public class HddRequestHandler : IRequestHandler<DateTimeRangeForHdd, List<HddMetricDto>>
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

        public Task<List<HddMetricDto>> Handle(DateTimeRangeForHdd request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Hdd Metrics: from - {request.FromTime}, to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new List<HddMetricDto>();

            foreach (var metric in metrics)
            {
                response.Add(_mapper.Map<HddMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
