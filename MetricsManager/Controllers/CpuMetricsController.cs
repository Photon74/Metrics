using AutoMapper;
using MediatR;
using MetricsManager.Client.Models;
using MetricsManager.Client.Responses;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ICpuMetricsRepository repository,
                                    IMapper mapper,
                                    ILogger<CpuMetricsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog is built in CpuMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] AgentIdTimePeriod agentIdTimePeriod)
{
            _logger.LogInformation($"Geting Cpu Metrics: " +
                $"from MetricsAgent - {agentIdTimePeriod.AgentId} " +
                $"from - {agentIdTimePeriod.FromTime}, " +
                $"to - {agentIdTimePeriod.ToTime}");

            var metrics = _repository.GetByTimePeriodFromAgent(agentIdTimePeriod);
            var response = new CpuMetricsApiResponse { Metrics = new List<CpuMetricDto>() };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            return Ok(response);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimePeriod timePeriod)
        {
            _logger.LogInformation($"Geting Cpu Metrics: " +
                $"from all MetricsAgent " +
                $"from - {timePeriod.FromTime}, " +
                $"to - {timePeriod.ToTime}");

            var metrics = _repository.GetByTimePeriod(timePeriod);

            var response = new CpuMetricsApiResponse { Metrics = new List<CpuMetricDto>() };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }
            return Ok(response);
        }
    }
}
