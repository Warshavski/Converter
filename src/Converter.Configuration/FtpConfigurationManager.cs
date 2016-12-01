using System;

using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;

using Escyug.Converter.Configuration.Sections;
using Escyug.Converter.Models.Repositories.Exceptions;

namespace Escyug.Converter.Configuration
{
    public class FtpConfigurationManager : IConfigurationManager<FtpConfiguration>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly System.Configuration.Configuration _configuration;
        


        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public FtpConfigurationManager(System.Configuration.Configuration configuration)
        {
            _configuration = configuration;
            
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IConfigurationManager<FtpConfiguration>

        //*** check for exceptions
        public FtpConfiguration Get()
        {
            try
            {
                var ftpConfigurationSection = GetFtpSection(_configuration);
                var ftpConfiguration = new FtpConfiguration
                {
                    Uri = ftpConfigurationSection.Host.Address,
                    Port = ftpConfigurationSection.Host.Port ?? 21,
                    User = ftpConfigurationSection.Credentials.User,
                    Password = ftpConfigurationSection.Credentials.Password
                };

                return ftpConfiguration;
            }
            catch (NullReferenceException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }

        //*** change receivers 
        public void Update(FtpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            try
            {
                var ftpConfigurationSection = GetFtpSection(_configuration);
                ftpConfigurationSection.Host.Address = configuration.Uri;
                ftpConfigurationSection.Host.Port = configuration.Port;

                ftpConfigurationSection.Credentials.User = configuration.User;
                ftpConfigurationSection.Credentials.Password = configuration.Password;

                _configuration.Save();
            }
            catch (NullReferenceException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }


        //*** NOT USED
        public FtpConfiguration Get(string name)
        {
            throw new System.NotImplementedException();
        }

        //*** NOT USED
        public void Set(FtpConfiguration entity)
        {
            throw new System.NotImplementedException();
        }

        #endregion IConfigurationManager<FtpConfiguration>
        


        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        private FtpSectionConfiguration GetFtpSection(System.Configuration.Configuration configuration)
        {
           var ftpConfigurationSection =
               configuration.Sections["ftpConfiguration"] as FtpSectionConfiguration;

            return ftpConfigurationSection;
        }
    }
}
