using MediatR;
using MetricsAgent.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CpuMetricsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeRangeForCpu dateTimeRange)
        {
            return Ok(_mediator.Send(dateTimeRange).Result);
        }
    }
}
