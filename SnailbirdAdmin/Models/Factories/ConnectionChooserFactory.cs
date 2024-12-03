using DataAccess;
using NetBlocks.Models.Environment;


namespace SnailbirdAdmin
{
    public static class ConnectionChooserFactory
    {
        public static ConnectionChooserModel BuildConnectionChooserModel<TDatabase>(IDataAccess<TDatabase> dataAccess)
        {
            Connections? connections = ConnectionStringTools.LoadFromFile("./environment/connections.json");

            IEnumerable<string>? names = null;
            var nameFetchResult = dataAccess.GetDatabaseNames();
            if (nameFetchResult.Success)
            {
                names = nameFetchResult.Value;
            }
            IEnumerable<string> databaseNames = names is not null && names.Any() ? new List<string>(names) : new List<string>();

            if (connections is not null && names is not null)
            {
                Connection connection = connections.ConnectionStrings.First(c => c.ConnectionString == dataAccess.GetConnectionString().Value);
                string databaseName = dataAccess.GetDatabaseName().Value;

                return new ConnectionChooserModel(connections.ConnectionStrings, 
                                                  databaseNames,
                                                  connection,
                                                  databaseName);
            }

            throw new Exception();
        }
    }
}
