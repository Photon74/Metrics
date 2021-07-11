using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.DAL.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly IDBConnectionManager _connection;

        public AgentRepository(IDBConnectionManager connection)
        {
            _connection = connection;
        }

        public void DisableAgent(int id)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute("UPDATE agents SET enabled = false WHERE agentId = @agentId", id);
        }

        public void EnableAgent(int id)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute("UPDATE agents SET enabled = true WHERE agentId = @agentId", id);
        }

        public IList<Agent> GetAllAgents()
        {
            using var connection = _connection.CreateOpenedConnection();
            var res = connection.Query<Agent>("SELECT * FROM agents WHERE enabled = 1").ToList();
            return res;
        }

        public void RegisterAgent(Agent agent)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute(
                "INSERT INTO agents(agentId, agentUrl, enabled) VALUES(@agentId, @agentUrl, @enabled)",
                new 
                {
                    agentId = agent.AgentId,
                    agentUrl = agent.AgentAddress,
                    enabled = true
                });
        }

        public void RemoveAgent(int id)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute("DELETE FROM agents WHERE agentId = @agentId", id);
        }
    }
}
