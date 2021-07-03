using AutoMapper;
using MetricsAgent.Controllers.Models;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger,
                                        INetworkMetricsRepository repository,
                                        IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog is built into CpuMetricsController");
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime,
                                        [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Get Network Metrics: fromTime - {fromTime}, toTime - {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new NetworkMetricsResponse
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
