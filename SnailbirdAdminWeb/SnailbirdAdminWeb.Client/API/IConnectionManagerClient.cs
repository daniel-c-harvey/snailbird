using NetBlocks.Models;
using NetBlocks.Models.Environment;

namespace SnailbirdAdminWeb.Client.API
{
    public interface IConnectionManagerClient
    {
        Task<ResultContainer<Connections>> GetConnections();
        Task<Result> SaveConnections(Connections connections);
    }
}