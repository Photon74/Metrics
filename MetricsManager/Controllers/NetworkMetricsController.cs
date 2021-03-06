using MediatR;
using MetricsManager.DAL.Models;
using MetricsManager.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NetworkMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("agent/{agentId}/from/{FromTime}/to/{ToTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] AgentIdTimePeriodNetworkRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }

        [HttpGet("cluster/from/{FromTime}/to/{ToTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimePeriodNetworkRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }
    }
}
