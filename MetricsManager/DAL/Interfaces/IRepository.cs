using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriodFromAgent (AgentTimePeriod period);
        IList<T> GetByTimePeriod(TimePeriod period);
        DateTimeOffset GetLastDate(int agentId);
        void Create(T item);
    }
}
