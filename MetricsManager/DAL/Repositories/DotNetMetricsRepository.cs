using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.DAL.Repositories
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly IDBConnectionManager _connection;

        public DotNetMetricsRepository(IDBConnectionManager connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(DotNetMetrics item)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute(
                "INSERT INTO dotnetmetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    agentId = item.AgentId
                });
        }

        public IList<DotNetMetrics> GetByTimePeriod(TimePeriod period)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<DotNetMetrics>(
                "SELECT * FROM dotnetmetrics WHERE time BETWEEN @FromTime AND @ToTime",
                new
                {
                    FromTime = period.FromTime.ToUnixTimeSeconds(),
                    ToTime = period.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public IList<DotNetMetrics> GetByTimePeriodFromAgent(AgentIdTimePeriod period)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<DotNetMetrics>(
                "SELECT * FROM dotnetmetrics WHERE agentId = @agentId AND time BETWEEN @FromTime AND @ToTime",
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

            return connection.QuerySingle<DateTimeOffset>("Select ifnull(max(time),0) from dotnetmetrics");
        }
    }
}
