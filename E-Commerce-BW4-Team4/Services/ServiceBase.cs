using System.Data.Common;

namespace E_Commerce_BW4_Team4.Services
{
    public abstract class ServiceBase
    {
        protected abstract DbConnection GetConnection();
        protected abstract DbCommand GetCommand(string commandText);

    }
}
