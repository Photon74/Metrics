using MediatR;
using MetricsManager.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] AgentIdTimePeriodRamRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] TimePeriodRamRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
