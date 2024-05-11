using Core;
using DataAccess;

namespace SnailbirdAdmin
{
    public enum ConnectionChooserAction
    {
        Unimplemented,
        ChangeConnection,
        ChangeDatabase
    }

    public abstract class ConnectionChooserMessage<TDatabase>
    {
        public IDataAccess<TDatabase> DataAccess { get; }
        public virtual ConnectionChooserAction Action => ConnectionChooserAction.Unimplemented;

        public ConnectionChooserMessage(IDataAccess<TDatabase> dataAccess)
        {
            DataAccess = dataAccess;
        }
    }

    public class ConnectionChooserChangeConnectionMessage<TDatabase> : ConnectionChooserMessage<TDatabase>
    {
        public override ConnectionChooserAction Action => ConnectionChooserAction.ChangeConnection;
        public long ConnectionID { get; private set; }

        public ConnectionChooserChangeConnectionMessage(IDataAccess<TDatabase> dataAccess, long connectionID)
            : base(dataAccess)
        {
            ConnectionID = connectionID;
        }
    }
    
    public class ConnectionChooserChangeDatabaseMessage<TDatabase> : ConnectionChooserMessage<TDatabase>
    {
        public override ConnectionChooserAction Action => ConnectionChooserAction.ChangeDatabase;
        public string DatabaseName { get; private set; }

        public ConnectionChooserChangeDatabaseMessage(IDataAccess<TDatabase> dataAccess, string databaseName)
            : base(dataAccess)
        {
            DatabaseName = databaseName;
        }
    }
}
