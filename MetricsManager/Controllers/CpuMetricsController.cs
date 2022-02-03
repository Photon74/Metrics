using MediatR;
using MetricsManager.Mediator.Requests;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
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

        [HttpGet("agent/{agentId}/from/{FromTime}/to/{ToTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] AgentIdTimePeriodCpuRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }
        /// <summary>
        /// Получает метрики CPU на заданном диапазоне времени
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET cpumetrics/from/1/to/9999999999
        ///
        /// </remarks>
        /// <param name="FromTime">начальная метрка времени в секундах с 01.01.1970</param>
        /// <param name="ToTime">конечная метрка времени в секундах с 01.01.1970</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="200">Если все хорошо</response>
        /// <response code="400">Если передали неправильные параметры</response>
        [HttpGet("cluster/from/{FromTime}/to/{ToTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimePeriodCpuRequest request)
        {
            return Ok(_mediator.Send(request).Result);
        }
    }
}
