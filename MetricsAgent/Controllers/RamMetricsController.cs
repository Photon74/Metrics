using AutoMapper;
using MetricsAgent.Controllers.Models;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
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
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger,
                                    IRamMetricsRepository repository,
                                    IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog is built into CpuMetricsController");
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime,
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
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return Ok(response);
        }

    }
}
