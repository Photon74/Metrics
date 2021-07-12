using System.Data;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IDBConnectionManager
    {
        IDbConnection CreateOpenedConnection();
    }
}
