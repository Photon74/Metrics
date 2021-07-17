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
    public class NetworkRequestHandler : IRequestHandler<TimePeriodNetworkRequest, NetworkMetricsResponse>
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly ILogger<NetworkRequestHandler> _logger;
        private readonly IMapper _mapper;

        public NetworkRequestHandler(INetworkMetricsRepository repository,
                                 ILogger<NetworkRequestHandler> logger,
                                 IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<NetworkMetricsResponse> Handle(TimePeriodNetworkRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Geting Network Metrics: " +
                $"from all MetricsAgent " +
                $"from - {request.FromTime}, " +
                $"to - {request.ToTime}");

            var metrics = _repository.GetByTimePeriod(_mapper.Map<TimePeriod>(request));

            var response = new NetworkMetricsResponse { Metrics = new List<NetworkMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            return Task.FromResult(response);
        }
    }
}
