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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger,
                                       IDotNetMetricsRepository repository,
                                       IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog is built into DotNetMetricsController");
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime,
                                        [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Get DotNet Metrics: fromTime - {fromTime}, toTime - {toTime}");

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new DotNetMetricsResponse
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
