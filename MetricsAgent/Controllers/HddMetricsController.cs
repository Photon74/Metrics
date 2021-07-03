using AutoMapper;
using MetricsAgent.Controllers.Models;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _repository;
        private readonly IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger,
                                    IHddMetricsRepository repository,
                                    IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog is built into HddMetricsController");
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime,
                                        [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Get Hdd Metrics: fromTime - {fromTime}, toTime - {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new HddMetricsResponse
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
