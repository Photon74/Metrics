using MetricsAgent.DAL.Interfaces;
using System.Data;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class SQLiteConnectionManager : IDBConnectionManager
    {
        public string ConnectionString => "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IDbConnection CreateOpenedConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            return connection;
        }
    }
}
