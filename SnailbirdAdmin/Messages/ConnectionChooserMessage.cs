using DataAccess;
using SnailbirdAdmin.Messages;

namespace SnailbirdAdmin
{
    public enum ConnectionChooserAction
    {
        Unimplemented,
        ChangeConnection,
        ChangeDatabase
    }

    public abstract class ConnectionChooserMessage<TDatabase> : MessageBase<ConnectionChooserAction>
    {
        public IDataAccess<TDatabase> DataAccess { get; }

        public ConnectionChooserMessage(ConnectionChooserAction action, IDataAccess<TDatabase> dataAccess)
        : base(action)
        {
            DataAccess = dataAccess;
        }
    }

    public class ConnectionChooserChangeConnectionMessage<TDatabase> : ConnectionChooserMessage<TDatabase>
    {
        public long ConnectionID { get; private set; }

        public ConnectionChooserChangeConnectionMessage(IDataAccess<TDatabase> dataAccess, long connectionID)
            : base(ConnectionChooserAction.ChangeConnection, dataAccess)
        {
            ConnectionID = connectionID;
        }
    }
    
    public class ConnectionChooserChangeDatabaseMessage<TDatabase> : ConnectionChooserMessage<TDatabase>
    {
        public string DatabaseName { get; private set; }

        public ConnectionChooserChangeDatabaseMessage(IDataAccess<TDatabase> dataAccess, string databaseName)
            : base(ConnectionChooserAction.ChangeDatabase, dataAccess)
        {
            DatabaseName = databaseName;
        }
    }
}
