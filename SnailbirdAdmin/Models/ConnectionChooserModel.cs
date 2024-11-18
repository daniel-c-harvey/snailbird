using NetBlocks.Models;

namespace SnailbirdAdmin
{
    public class ConnectionChooserModel
    {
        public IEnumerable<Connection> Connections { get; set; }
        public IEnumerable<string> DatabaseNames { get; set; }

        public Connection Connection { get; set; }
        public string DatabaseName { get; set; }

        public ConnectionChooserModel(IEnumerable<Connection> connections, 
                                      IEnumerable<string> names,
                                      Connection connection,
                                      string databaseName)
        {
            Connections = connections.ToList();
            DatabaseNames = names.ToList();
            Connection = connection;
            DatabaseName = databaseName;
        }
    }
}
