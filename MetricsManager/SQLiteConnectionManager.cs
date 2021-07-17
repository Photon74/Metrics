using MetricsManager.DAL.Interfaces;
using System.Data;
using System.Data.SQLite;

namespace MetricsManager
{
    public class SQLiteConnectionManager : IDBConnectionManager
    {
        public SQLiteConnection connection;
        public string ConnectionString => "Data Source=metricsManager.db;Version=3;Pooling=true;Max Pool Size=100";
        public IDbConnection CreateOpenedConnection()
        {
            //lock (_locker)
            //{
            //    if (connection == null)
            //    {
                    connection = new SQLiteConnection(ConnectionString);
                    connection.Open();
            //    }
            //}
            return connection;
        }
        private object _locker = new object();
    }
}
