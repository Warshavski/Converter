using System;

using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Repositories.Exceptions;

namespace Escyug.Converter.Configuration
{
    public class ConnectionStringsConfigurationManager : IConfigurationManager<ConnectionStringConfiguration>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly System.Configuration.Configuration _config;



        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public ConnectionStringsConfigurationManager(System.Configuration.Configuration config)
        {
            _config = config;
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IConfigurationManager<ConnectionStringConfiguration>

        public ConnectionStringConfiguration Get(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
            {
                throw new ArgumentNullException(nameof(connectionStringName));
            }

            try
            {
                var connectionString =
                    _config.ConnectionStrings.ConnectionStrings[connectionStringName].ConnectionString;
                var providerName =
                    _config.ConnectionStrings.ConnectionStrings[connectionStringName].ProviderName;

                var rootFolder = AppDomain.CurrentDomain.BaseDirectory;
                var rep = connectionString.Replace("{AppDir}", rootFolder);

                var connectionStringConfig = new ConnectionStringConfiguration(connectionStringName)
                {
                    ConnectionString = rep,
                    ProviderName = providerName
                };
                return connectionStringConfig;
            }
            catch (NullReferenceException)
            {
                return new ConnectionStringConfiguration(connectionStringName)
                {
                    ConnectionString = string.Empty,
                    ProviderName = string.Empty
                };
            }
        }

        public void Update(ConnectionStringConfiguration connectionStringConfiguration)
        {
            if (connectionStringConfiguration == null)
            {
                throw new ArgumentNullException(nameof(connectionStringConfiguration));
            }

            try
            {
                _config.ConnectionStrings.ConnectionStrings[connectionStringConfiguration.Name].ConnectionString =
                    connectionStringConfiguration.ConnectionString;

                _config.ConnectionStrings.ConnectionStrings[connectionStringConfiguration.Name].ProviderName =
                    connectionStringConfiguration.ProviderName;

                _config.Save(System.Configuration.ConfigurationSaveMode.Modified, true);
                System.Configuration.ConfigurationManager.RefreshSection("connectionStrings");
            }
            catch (NullReferenceException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }

        //*** NOT USED
        public ConnectionStringConfiguration Get()
        {
            throw new NotImplementedException();
        }

        //*** NOT USED
        public void Set(ConnectionStringConfiguration entity)
        {
            throw new NotImplementedException();
        }

        #endregion IConfigurationManager<ConnectionStringConfiguration>
    }
}
