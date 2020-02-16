using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.Entity;

namespace MatrixCRM.Models
{
    public class ConnectionHelper : DbContext
    {
        public ConnectionHelper() : base(ConnectionHelper.ConnectionStringName) { }
        public static string ConnectionStringName { get; set; } = setDefaultConnectionString();

        public static string CreateConnectionString(string metaData, string dataSource, string initialCatalog, string UserID, string Password)
        {
            const string appName = "EntityFramework";
            const string providerName = "System.Data.SqlClient";

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = dataSource;
            sqlBuilder.InitialCatalog = initialCatalog;
            sqlBuilder.MultipleActiveResultSets = true;
            //sqlBuilder.IntegratedSecurity = true;
            sqlBuilder.ApplicationName = appName;

            EntityConnectionStringBuilder efBuilder = new EntityConnectionStringBuilder();
            efBuilder.Metadata = metaData;
            efBuilder.Provider = providerName;
            efBuilder.ProviderConnectionString = sqlBuilder.ConnectionString;

            return efBuilder.ConnectionString;
        }
        public static string CreateConnectionString(string dataSource, string initialCatalog, string UserID, string Password)
        {
            string metaData = "res://*/Models.DB.csdl|res://*/Models.DB.ssdl|res://*/Models.DB.msl";

            const string appName = "EntityFramework";
            const string providerName = "System.Data.SqlClient";

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = dataSource;
            sqlBuilder.InitialCatalog = initialCatalog;
            sqlBuilder.UserID = UserID;
            sqlBuilder.Password = Password;

            sqlBuilder.MultipleActiveResultSets = true;
            // sqlBuilder.IntegratedSecurity = true;
            sqlBuilder.ApplicationName = appName;

            EntityConnectionStringBuilder efBuilder = new EntityConnectionStringBuilder();
            efBuilder.Metadata = metaData;
            efBuilder.Provider = providerName;
            efBuilder.ProviderConnectionString = sqlBuilder.ConnectionString;

            return efBuilder.ConnectionString;
        }
        public static void setNewConnectionString(string dataSource, string initialCatalog, string UserID, string Password)
        {
            // ConnectionStringName = "name=" + connectionString;
            ConnectionStringName = CreateConnectionString(dataSource, initialCatalog, UserID, Password);
        }
        public static string setDefaultConnectionString()
        {
            var DBServer = System.Configuration.ConfigurationManager.AppSettings["DefaultDBServer"];
            var DBName = System.Configuration.ConfigurationManager.AppSettings["DefaultDB"];
            var DefaultUser = System.Configuration.ConfigurationManager.AppSettings["DefaultUser"];
            var DefaultPassword = System.Configuration.ConfigurationManager.AppSettings["DefaultPassword"];

            return CreateConnectionString(DBServer, DBName, DefaultUser, DefaultPassword);
        }
    }
}
