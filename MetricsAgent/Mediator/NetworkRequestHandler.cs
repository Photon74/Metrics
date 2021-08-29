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
    public class NetworkRequestHandler : IRequestHandler<DateTimeRangeForNetwork, NetworkMetricsResponse>
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NetworkRequestHandler> _logger;

        public NetworkRequestHandler(INetworkMetricsRepository repository,
                                     IMapper mapper,
                                     ILogger<NetworkRequestHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<NetworkMetricsResponse> Handle(DateTimeRangeForNetwork request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Network Metrics: from - {request.FromTime}, to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(request.FromTime, request.ToTime);

            var response = new NetworkMetricsResponse { Metrics = new List<NetworkMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
