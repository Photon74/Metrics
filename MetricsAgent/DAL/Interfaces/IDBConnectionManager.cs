using System.Data;
using System.Data.SQLite;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IDBConnectionManager
    {
        IDbConnection CreateOpenedConnection();
    }
}
