using AutoMapper;
using MetricsAgent.Controllers.Models;
using MetricsAgent.Controllers.Responses;
using MetricsAgent.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger,
                                    ICpuMetricsRepository repository,
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
            _logger.LogInformation($"Get Cpu Metrics: fromTime - {fromTime}, toTime - {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new CpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
