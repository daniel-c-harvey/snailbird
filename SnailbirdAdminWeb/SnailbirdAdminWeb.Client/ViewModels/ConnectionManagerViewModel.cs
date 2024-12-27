using NetBlocks.Models;
using NetBlocks.Models.Environment;
using NetBlocks.Utilities;
using RazorCore;
using SnailbirdAdminWeb.Client.API;
using SnailbirdAdminWeb.Client.Views;

namespace SnailbirdAdminWeb.Client.ViewModels
{
    public class ConnectionManagerViewModel
    {
        public async static Task<ConnectionManagerViewModel> Init(IConnectionManagerClient connectionManagerClient)
        {
            ConnectionManagerViewModel viewModel = new(connectionManagerClient);
            ResultContainer<Connections> connResults = await connectionManagerClient.GetConnections();
            viewModel.Connections = connResults.Value ?? new();
            return viewModel;
        }

        public static IColumnMap<Connection> ColumnMap = new ColumnMap<Connection>()
            .AddColumn("ID",
                new ModelColumn<Connection>(
                    (c) => LongConverter.ToString(c.ID),
                    (c, id) => c.ID = LongConverter.FromString(id))
                .MakeEditable())
            .AddColumn("Name",
                new ModelColumn<Connection>(
                    (c) => c.ConnectionName,
                    (c, name) => c.ConnectionName = name)
                .MakeEditable())
            .AddColumn("Connection String",
                new ModelColumn<Connection>(
                    (c) => c.ConnectionString,
                    (c, connctionString) => c.ConnectionString = connctionString)
                .MakeEditable());

        private IConnectionManagerClient connectionManagerClient;
        public Connections Connections { get; private set; } = default!;

        private ConnectionManagerViewModel(IConnectionManagerClient client)
        {
            connectionManagerClient = client;
        }

        public void Delete(Connection connection)
        {
            Connections.ConnectionStrings.Remove(connection);
        }

        public void Add(Connection connection)
        {
            Connections.ConnectionStrings.Add(connection);
        }

        public void SaveConnections()
        {
            connectionManagerClient.SaveConnections(Connections);
        }
    }
}
