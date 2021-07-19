using AutoMapper;
using MetricsManager.Controllers.Models;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentRepository _agentRepository;
        private readonly IMapper _mapper;
        public AgentsController(IAgentRepository agentRepository,
                                IMapper mapper,
                                ILogger<AgentsController> logger)
        {
            _agentRepository = agentRepository;
            _mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog is built in AgentsController");
        }
        /// <summary>
        /// Регистрация Агента сбора метрик
        /// </summary>
        /// <param name="AgentId">Идентификатор агента</param>
        /// <param name="AgentUrl">Сетевой адрес агента</param>
        /// <param name="Enabled">Активность агента</param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation($"Register MetricsAgent - {agentInfo.AgentId}, Url - {agentInfo.AgentUrl}");

            _agentRepository.RegisterAgent(_mapper.Map<Agent>(agentInfo));

            return Ok($"{agentInfo.AgentId}, {agentInfo.AgentUrl}, {agentInfo.Enabled}");
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Enabled MetricsAgent - {agentId}");

            _agentRepository.EnableAgent(agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Disabled MetricsAgent - {agentId}");

            _agentRepository.DisableAgent(agentId);
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult GetAgentsList()
        {
            _logger.LogInformation($"Getting list of MetricsAgents");

            return Ok(_agentRepository.GetAllAgents());
        }

        [HttpPut("delete/{agentId}")]
        public IActionResult DeleteAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Deleted MetricsAgent - {agentId}");

            _agentRepository.RemoveAgent(agentId);
            return Ok();
        }
    }
}
