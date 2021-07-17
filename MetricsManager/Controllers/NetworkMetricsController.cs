using AutoMapper;
using MetricsManager.Client.Models;
using MetricsManager.Client.Responses;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(INetworkMetricsRepository repository,
                                        IMapper mapper,
                                        ILogger<NetworkMetricsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog is built in NetworkMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] AgentIdTimePeriod agentIdTimePeriod)
        {
            _logger.LogInformation($"Geting Network Metrics: " +
                $"from MetricsAgent - {agentIdTimePeriod.AgentId} " +
                $"from - {agentIdTimePeriod.FromTime}, " +
                $"to - {agentIdTimePeriod.ToTime}");

            var metrics = _repository.GetByTimePeriodFromAgent(agentIdTimePeriod);

            var response = new NetworkMetricsApiResponse { Metrics = new List<NetworkMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimePeriod timePeriod)
        {
            _logger.LogInformation($"Geting Network Metrics: " +
                $"from all MetricsAgent " +
                $"from - {timePeriod.FromTime}, " +
                $"to - {timePeriod.ToTime}");

            var metrics = _repository.GetByTimePeriod(timePeriod);

            var response = new NetworkMetricsApiResponse { Metrics = new List<NetworkMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }
            return Ok(response);
        }
    }
}
