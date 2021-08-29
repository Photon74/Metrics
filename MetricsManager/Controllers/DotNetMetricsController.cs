using MediatR;
using MetricsManager.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] AgentIdTimePeriodDotNetRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] TimePeriodDotNetRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
