﻿using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repositories
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetrics>
    {
    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private readonly IDBConnectionManager _connectionManager;

        public DotNetMetricsRepository(IDBConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Create(DotNetMetrics item)
        {
            using var command = new SQLiteCommand((SQLiteConnection)_connectionManager.CreateOpenedConnection());

            command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)";
            command.Parameters.AddWithValue("@value", item.Value);
            command.Parameters.AddWithValue("@time", item.Time);
            command.Prepare();
            command.ExecuteNonQuery();
        }

        public IList<DotNetMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var command = new SQLiteCommand((SQLiteConnection)_connectionManager.CreateOpenedConnection());

            command.CommandText = "SELECT * FROM dotnetmetrics WHERE time BETWEEN @fromTime AND @toTime";
            command.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            command.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            command.Prepare();

            var result = new List<DotNetMetrics>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new DotNetMetrics
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        Time = reader.GetInt64(2)
                    });
                }
            }
            return result;
        }
    }
}
