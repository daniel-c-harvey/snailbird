using NetBlocks.Models;
using NetBlocks.Models.Environment;

namespace SnailbirdAdminWeb.API.Managers
{
    public interface IConnectionManager
    {
        ResultContainer<Connections> GetConnections();
        Result SaveConnections(Connections connections);
    }
}