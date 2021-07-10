using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.DAL.Repositories
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private readonly IDBConnectionManager _connection;

        public HddMetricsRepository(IDBConnectionManager connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(HddMetrics item)
        {
            using var connection = _connection.CreateOpenedConnection();

            connection.Execute("INSERT INTO hddmetrics(value, time, agentId) VALUES(@value, @time, @agentId)",
                (
                    value: item.Value,
                    time: item.Time,
                    agentId: item.AgentId
                ));
        }

        public IList<HddMetrics> GetByTimePeriod(TimePeriod period)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<HddMetrics>(
                "SELECT * FROM hddmetrics WHERE time BETWEEN @fromTime AND @toTime",
                (
                    fromTime: period.FromTime.ToUnixTimeSeconds(),
                    toTime: period.ToTime.ToUnixTimeSeconds()
                )).ToList();
        }

        public IList<HddMetrics> GetByTimePeriodFromAgent(AgentIdTimePeriod period)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<HddMetrics>(
                "SELECT * FROM hddmetrics WHERE agentId = @agentId AND time BETWEEN @fromTime AND @toTime",
                (
                    agentId: period.AgentId,
                    fromTime: period.FromTime,
                    toTime: period.ToTime
                )).ToList();
        }

        public DateTimeOffset GetLastDate(int agentId)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.QuerySingle<DateTimeOffset>("Select isnull(max(time),0) from hddmetrics");
        }
    }
}
