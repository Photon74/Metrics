using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsAgent.DAL.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly IDBConnectionManager _connection;

        public NetworkMetricsRepository(IDBConnectionManager connection)
        {
            _connection = connection;
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(NetworkMetrics item)
        {
            using var connection = _connection.CreateOpenedConnection();
            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public IList<NetworkMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<NetworkMetrics>("SELECT * FROM networkmetrics WHERE time BETWEEN @fromTime AND @toTime",
                new
                {
                    fromTime = fromTime.ToUnixTimeSeconds(),
                    toTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }
    }
}
