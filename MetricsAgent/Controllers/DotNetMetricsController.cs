using MediatR;
using MetricsAgent.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
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

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeRangeForDotNet dateTimeRange)
        {
            return Ok(_mediator.Send(dateTimeRange).Result);
        }
    }
}
