﻿using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repositories
{
    public interface IRamMetricsRepository : IRepository<RamMetrics>
    {
    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string ConnectionString = ConnectionStringToDataBase.ConnectionString;

        public void Create(RamMetrics item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);

            command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";
            command.Parameters.AddWithValue("@value", item.Value);
            command.Parameters.AddWithValue("@time", item.Time);
            command.Prepare();
            command.ExecuteNonQuery();
        }

        public IList<RamMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(connection);

            command.CommandText = "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime";
            command.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            command.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            command.Prepare();

            var result = new List<RamMetrics>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new RamMetrics
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