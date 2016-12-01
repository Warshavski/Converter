using System;
using System.Data;
using System.Data.Common;

namespace Escyug.Converter.Data.Ado
{
    /// <summary>
    ///     Context for data access
    /// </summary>
    public class DbContext
    {
        /// <summary>
        ///     Database connection string
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        ///     Database connection provider name
        /// </summary>
        public string ProviderName { get; }

        //public DbConnection Conneciton { get; private set; }

        /// <summary>
        ///     Create new database context
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="providerName">Database connection provider name</param>
        public DbContext(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
        }


        // DB CONNECTION CREATE SECTION
        //---------------------------------------------------------------------

        /// <summary>
        ///     Creates DbConnection instance
        /// </summary>
        /// <returns>Created DbConnection</returns>
        public DbConnection CreateConnection()
        {
            var dbConnection = CreateConnection(ConnectionString, ProviderName);
            return dbConnection;
        }

        public DbConnection CreateConnection(string connectionString, string providerName)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (string.IsNullOrEmpty((providerName)))
            {
                throw new ArgumentNullException(nameof(providerName));
            }

            var factory = DbProviderFactories.GetFactory(providerName);

            DbConnection connection = factory.CreateConnection();
            if (connection != null)
            {
                connection.ConnectionString = connectionString;
            }

            return connection;
        }


        // DB COMMAND CREATE SECTION
        //---------------------------------------------------------------------

        /// <summary>
        ///     Create a new command
        /// </summary>
        /// <param name="connection">DbConnecton instance</param>
        /// <param name="commandText">Text of Dbcommand</param>
        /// <param name="commandType">Type of DbCommand</param>
        /// <returns>Created command (<c>null</c> if fail)</returns>
        public DbCommand CreateCommand(DbConnection connection,
            string commandText, CommandType commandType)
        {
            DbCommand command = CreateCommand(connection, commandText);

            command.CommandText = commandText;
            command.CommandType = commandType;

            return command;
        }


        /// <summary>
        ///     Create a new command
        /// </summary>
        /// <param name="connection">DbConnecton instance</param>
        /// <param name="commandText">Text of Dbcommand</param>
        /// <returns>Created command</returns>
        private DbCommand CreateCommand(DbConnection connection, string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new ArgumentNullException(nameof(connection));
            }

            DbCommand command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }
    }
}
