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
    public class DotNetRequestHandler : IRequestHandler<TimePeriodDotNetRequest, DotNetMetricsResponse>
    {
        private readonly IDotNetMetricsRepository _repository;
        private readonly ILogger<DotNetRequestHandler> _logger;
        private readonly IMapper _mapper;

        public DotNetRequestHandler(IDotNetMetricsRepository repository,
                                 ILogger<DotNetRequestHandler> logger,
                                 IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<DotNetMetricsResponse> Handle(TimePeriodDotNetRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting DotNet Metrics: " +
                $"from all MetricsAgent " +
                $"from - {request.FromTime}, " +
                $"to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(_mapper.Map<TimePeriod>(request));

            var response = new DotNetMetricsResponse { Metrics = new List<DotNetMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
