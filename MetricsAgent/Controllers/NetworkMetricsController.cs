using MediatR;
using MetricsAgent.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
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

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeRangeForNetwork dateTimeRange)
        {
            return Ok(_mediator.Send(dateTimeRange).Result);
        }
    }
}
