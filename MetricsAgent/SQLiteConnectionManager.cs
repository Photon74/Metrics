using MetricsAgent.DAL.Interfaces;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class SQLiteConnectionManager : IDBConnectionManager
    {
        private string ConnectionString => "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100";

        public IDbConnection CreateOpenedConnection()
        {
            SQLiteConnection connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            return connection;
        }
    }
}
