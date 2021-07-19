using MediatR;
using MetricsManager.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DotNetMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("agent/{agentId}/from/{FromTime}/to/{ToTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] AgentIdTimePeriodDotNetRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }

        [HttpGet("cluster/from/{FromTime}/to/{ToTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimePeriodDotNetRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }
    }
}
