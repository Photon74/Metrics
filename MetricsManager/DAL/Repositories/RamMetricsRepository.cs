using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.DAL.Repositories
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private readonly IDBConnectionManager _connection;

        public RamMetricsRepository(IDBConnectionManager connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(RamMetrics item)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute(
                "INSERT INTO rammetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    agentId = item.AgentId
                });
        }

        public IList<RamMetrics> GetByTimePeriod(TimePeriod period)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<RamMetrics>(
                "SELECT * FROM rammetrics WHERE time BETWEEN @FromTime AND @ToTime",
                new
                {
                    FromTime = period.FromTime.ToUnixTimeSeconds(),
                    ToTime = period.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public IList<RamMetrics> GetByTimePeriodFromAgent(AgentIdTimePeriod period)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<RamMetrics>(
                "SELECT * FROM rammetrics WHERE agentId = @agentId AND time BETWEEN @FromTime AND @ToTime",
                new
                {
                    agentId = period.AgentId,
                    FromTime = period.FromTime.ToUnixTimeSeconds(),
                    ToTime = period.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public DateTimeOffset GetLastDate(int agentId)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.QuerySingle<DateTimeOffset>("Select ifnull(max(time),0) from rammetrics");
        }
    }
}
