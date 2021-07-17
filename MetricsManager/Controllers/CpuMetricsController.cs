using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId,
                                                 [FromRoute] DateTimeOffset fromTime,
                                                 [FromRoute] DateTimeOffset toTime)
        {

            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime,
                                                      [FromRoute] DateTimeOffset toTime)
        {
            return Ok();
        }
    }
}
