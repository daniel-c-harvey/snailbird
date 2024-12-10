using DataAccess;
using SnailbirdAdminWeb.Models;
using SnailbirdAdminWeb.Messages;

namespace SnailbirdAdminWeb.Updates
{
    public static class ConnectionChooserUpdate<TDatabase>
    {
        public static ConnectionChooserModel Update(ConnectionChooserModel model, 
                                                    ConnectionChooserMessage<TDatabase> message)
        {
            switch (message.Action)
            {
                case ConnectionChooserAction.ChangeConnection:
                    var connectionMessage = message as ConnectionChooserChangeConnectionMessage<TDatabase>;
                    if (connectionMessage is not null)
                    {
                        UpdateConnection(model, connectionMessage);
                    }
                    break;

                case ConnectionChooserAction.ChangeDatabase:
                    var databaseMessage = message as ConnectionChooserChangeDatabaseMessage<TDatabase>;
                    if (databaseMessage is not null)
                    {
                        UpdateDatabase(model, databaseMessage);
                    }
                    break;

                case ConnectionChooserAction.Unimplemented:
                default:
                    throw new NotImplementedException();
            }
            return model;
        }

        private static void UpdateDatabase(ConnectionChooserModel model, 
                                           ConnectionChooserChangeDatabaseMessage<TDatabase> message)
        {
            model.DatabaseName = message.DatabaseName;
            OnConnectionChange(model, message);
        }

        private static void UpdateConnection(ConnectionChooserModel model, 
                                             ConnectionChooserChangeConnectionMessage<TDatabase> message)
        {
            if (message is not null && model.Connections is not null)
            {
                var connection = model.Connections.FirstOrDefault(c => c.ID == message.ConnectionID);
                if (connection != null)
                {
                    model.Connection = connection;
                    OnConnectionChange(model, message);
                }
            }
        }

        private static void OnConnectionChange(ConnectionChooserModel model,
                                               ConnectionChooserMessage<TDatabase> message)
        {
            if (message.DataAccess is null) throw new ArgumentNullException(nameof(DataAccess));

            if (model.Connection is not null && !string.IsNullOrWhiteSpace(model.DatabaseName))
            {
                message.DataAccess.ChangeConnection(model.Connection, model.DatabaseName);
            }
        }
    }
}
