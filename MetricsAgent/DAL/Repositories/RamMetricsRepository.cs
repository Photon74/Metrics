using Dapper;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL.Repositories
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
            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time
                });
        }

        public IList<RamMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = _connection.CreateOpenedConnection();

            return connection.Query<RamMetrics>("SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime",
                new
                {
                    fromTime = fromTime.ToUnixTimeSeconds(),
                    toTime = toTime.ToUnixTimeSeconds()
                }).ToList();
        }

    }
}
