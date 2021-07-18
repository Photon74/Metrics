using MediatR;
using MetricsManager.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RamMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] AgentIdTimePeriodRamRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimePeriodRamRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }
    }
}
