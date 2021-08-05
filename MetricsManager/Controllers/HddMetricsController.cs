using MediatR;
using MetricsManager.DAL.Models;
using MetricsManager.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAgent([FromRoute] AgentIdTimePeriodHddRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public async Task<IActionResult> GetMetricsFromAllCluster([FromRoute] TimePeriodHddRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
