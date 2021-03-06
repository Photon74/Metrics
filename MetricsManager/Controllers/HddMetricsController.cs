using MediatR;
using MetricsManager.DAL.Models;
using MetricsManager.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HddMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("agent/{agentId}/from/{FromTime}/to/{ToTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] AgentIdTimePeriodHddRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }

        [HttpGet("cluster/from/{FromTime}/to/{ToTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimePeriodHddRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }
    }
}
