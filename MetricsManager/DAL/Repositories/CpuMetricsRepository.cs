using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.DAL.Repositories
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly IDBConnectionManager _connection;

        public CpuMetricsRepository(IDBConnectionManager connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetrics item)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute(
                "INSERT INTO cpumetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
                new
                {
                    value = item.Value,
                    time = item.Time,
                    agentId = item.AgentId
                });
        }

        public IList<CpuMetrics> GetByTimePeriod(TimePeriod period)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<CpuMetrics>(
                "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                new
                {
                    fromTime = period.FromTime.ToUnixTimeSeconds(),
                    toTime = period.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public IList<CpuMetrics> GetByTimePeriodFromAgent(AgentIdTimePeriod period)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<CpuMetrics>(
                "SELECT * FROM cpumetrics WHERE agentId = @agentId AND time BETWEEN @fromTime AND @toTime",
                new
                {
                    agentId = period.AgentId,
                    fromTime = period.FromTime.ToUnixTimeSeconds(),
                    toTime = period.ToTime.ToUnixTimeSeconds()
                }).ToList();
        }

        public DateTimeOffset GetLastDate(int agentId)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.QuerySingle<DateTimeOffset>("Select ifnull(max(Time),0) from cpumetrics");
        }
    }
}
