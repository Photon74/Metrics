using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Repositories
{
    public interface ICpuMetricsRepository : IRepository<CpuMetrics>
    {
    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private readonly IDBConnectionManager _connection;

        public CpuMetricsRepository(IDBConnectionManager connection)
        {
            _connection = connection;
        }

        public void Create(CpuMetrics item)
        {
            using var command = new SQLiteCommand(_connection.CreateOpenedConnection() as SQLiteConnection);

            command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";
            command.Parameters.AddWithValue("@value", item.Value);
            command.Parameters.AddWithValue("@time", item.Time);
            command.Prepare();
            command.ExecuteNonQuery();
        }

        public IList<CpuMetrics> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var command = new SQLiteCommand((SQLiteConnection)_connection.CreateOpenedConnection());

            command.CommandText = "SELECT * FROM cpumetrics WHERE time BETWEEN @fromTime AND @toTime";
            command.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            command.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            command.Prepare();

            var result = new List<CpuMetrics>();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new CpuMetrics
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
