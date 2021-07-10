using MetricsManager.DAL.Models;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    interface IAgentRepository
    {
        IList<Agent> GetAllAgents();
        void RegisterAgent(Agent agent);
        void RemoveAgent(int id);
        void EnableAgent(int id);
        void DisableAgent(int id);
    }
}
