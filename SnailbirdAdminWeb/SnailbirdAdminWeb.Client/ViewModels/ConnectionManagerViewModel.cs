using NetBlocks.Models;
using NetBlocks.Models.Environment;
using NetBlocks.Utilities;
using RazorCore.Table;
using SnailbirdAdminWeb.Client.API;

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

        public IColumnMap<Connection> ColumnMap;

        private IConnectionManagerClient connectionManagerClient;
        public Connections Connections { get; private set; } = default!;

        private ConnectionManagerViewModel(IConnectionManagerClient client)
        {
            connectionManagerClient = client;

            ColumnMap = new ColumnMap<Connection>()
            .AddColumn(
                ColumnKey.Init("Active Connection", typeof(bool)),
                new ModelColumn<Connection, bool>(
                    (c) => Connections.ActiveConnectionID == c.ID,
                    (c, value) => {
                        if (value) Connections.ActiveConnectionID = c.ID;
                    })
                .WithCheckable())
            .AddColumn(
                ColumnKey.Init(
                    typeof(Connection).GetProperty(nameof(Connection.ID))),
                new ModelColumn<Connection, int>(
                    (c) => c.ID,
                    (c, id) => c.ID = id)
                .WithEditable())
            .AddColumn(
                ColumnKey.Init(
                    "Connection Name", 
                    typeof(Connection).GetProperty(nameof(Connection.ConnectionName))),
                new ModelColumn<Connection, string>(
                    (c) => c.ConnectionName,
                    (c, name) => c.ConnectionName = name)
                .WithEditable())
            .AddColumn(
                ColumnKey.Init(
                    "Connection String", 
                    typeof(Connection).GetProperty(nameof(Connection.ConnectionString))),
                new ModelColumn<Connection, string>(
                    (c) => c.ConnectionString,
                    (c, connctionString) => c.ConnectionString = connctionString)
                .WithEditable());
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
