using System.Data;

namespace MetricsManager.DAL.Interfaces
{
    public interface IDBConnectionManager
    {
        IDbConnection CreateOpenedConnection();
    }
}
