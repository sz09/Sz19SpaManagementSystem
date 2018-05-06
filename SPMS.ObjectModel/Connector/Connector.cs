namespace Core.ObjectModel.Entities
{
    using System.Configuration;
    using System.Data.Entity;

    public class Connector: DbContext
    {
        public static string _connectionString;

        public Connector()
            : base("name=SpaManagementEntities")
        {
            _connectionString = base.Database.Connection.ConnectionString;
        }
    }
}
