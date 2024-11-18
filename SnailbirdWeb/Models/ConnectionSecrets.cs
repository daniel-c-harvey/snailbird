namespace SnailbirdWeb.Models
{
    public class ConnectionSecrets
    {
        public string ActiveConnection { get; set; } = default!;
        public IEnumerable<Connection> Connections { get; set; } = default!;
    }

    public class Connection
    {
        public string Name { get; set; } = default!;
        public string ConnectionString { get; set; } = default!;
    }
}
