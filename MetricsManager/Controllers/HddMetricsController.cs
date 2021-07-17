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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly IHddMetricsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(IHddMetricsRepository repository,
                                    IMapper mapper,
                                    ILogger<HddMetricsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog is built in HddMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] AgentIdTimePeriod agentIdTimePeriod)
        {
            _logger.LogInformation($"Geting Hdd Metrics: " +
                $"from MetricsAgent - {agentIdTimePeriod.AgentId} " +
                $"from - {agentIdTimePeriod.FromTime}, " +
                $"to - {agentIdTimePeriod.ToTime}");

            var metrics = _repository.GetByTimePeriodFromAgent(agentIdTimePeriod);

            var response = new HddMetricsApiResponse { Metrics = new List<HddMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }
            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimePeriod timePeriod)
        {
            _logger.LogInformation($"Geting Hdd Metrics: " +
                $"from all MetricsAgent " +
                $"from - {timePeriod.FromTime}, " +
                $"to - {timePeriod.ToTime}");

            var metrics = _repository.GetByTimePeriod(timePeriod);

            var response = new HddMetricsApiResponse { Metrics = new List<HddMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }
            return Ok(response);
        }
    }
}
