using NetBlocks.Models;
using NetBlocks.Models.Environment;
using NetBlocks.Utilities.Environment;

namespace SnailbirdAdminWeb.API.Managers
{
    public class ConnectionManager : IConnectionManager
    {
        private const string FilePathFromBase = "environment/connections.json";

        private IConnectionStringLoader ConnectionStringLoader { get; set; }

        public ConnectionManager(IConnectionStringLoader connectionStringLoader)
        {
            ConnectionStringLoader = connectionStringLoader;
        }

        public ResultContainer<Connections> GetConnections()
        {
            try
            {
                Connections? connections = ConnectionStringLoader.LoadFromFile(FilePathFromBase);
                if (connections is null)
                {
                    throw new Exception("Failed to load connections file");
                }

                return new ResultContainer<Connections>(connections);
            }
            catch (Exception ex)
            {
                return ResultContainer<Connections>.CreateFailResult(ex.Message);
            }
        }

        public Result SaveConnections(Connections connections)
        {
            ConnectionStringLoader.SaveToFile(FilePathFromBase, connections);
            return Result.CreatePassResult();
        }
    }
}
