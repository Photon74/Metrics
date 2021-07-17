using AutoMapper;
using MetricsManager.Controllers.Models;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace MetricsManager.Controllers
{
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentRepository _agentRepository;
        private readonly IMapper _mapper;
        public AgentsController(IAgentRepository agentRepository, IMapper mapper)
        {
            _agentRepository = agentRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _agentRepository.RegisterAgent(_mapper.Map<Agent>(agentInfo));

            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _agentRepository.EnableAgent(agentId);
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _agentRepository.DisableAgent(agentId);
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult GetAgentsList()
        {
            var response = _agentRepository.GetAllAgents();
            return Ok(response);
        }

        [HttpPut("delete/{agentId}")]
        public IActionResult DeleteAgentById([FromRoute] int agentId)
        {
            _agentRepository.RemoveAgent(agentId);
            return Ok();
        }
    }
}
