using MetricsAgent.Controllers.Responses;
using MetricsAgent.DAL.Models;
using MetricsAgent.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _repository;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog is built into CpuMetricsController");
            _repository = repository;
        }
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics(
            [FromRoute] DateTimeOffset fromTime,
            [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Get Ram Metrics: fromTime - {fromTime}, toTime - {toTime}");

            IList<RamMetrics> metrics = _repository.GetByTimePeriod(fromTime, toTime);

            RamMetricsResponse response = new RamMetricsResponse
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto
                {
                    Id = metric.Id,
                    Value = metric.Value,
                    Time = DateTimeOffset.FromUnixTimeSeconds(metric.Time)
                });
            }

            return Ok(response);
        }

    }
}
