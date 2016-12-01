using System;

using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;

using Escyug.Converter.Configuration.Sections;
using Escyug.Converter.Models.Repositories.Exceptions;

namespace Escyug.Converter.Configuration
{
    public class SenderConfigurationManager : IConfigurationManager<SenderConfiguration>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly System.Configuration.Configuration _configuration;
        


        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public SenderConfigurationManager(System.Configuration.Configuration configuration)
        {
            _configuration = configuration;    
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IConfigurationManager<SenderConfiguration>

        //*** check for exceptions
        public SenderConfiguration Get()
        {
            try
            {
                var senderSection = GetSenderSection(_configuration);

                var senderConfiguration = new SenderConfiguration
                {
                    Host = senderSection.Host.Address,
                    Port = senderSection.Host.Port,
                    SenderLogin = senderSection.Credentials.Login,
                    SenderPassword = senderSection.Credentials.Password
                };


                int receiversTotal = senderSection.Receivers.Count;
                var receivers = new string[receiversTotal];
                for (int receiversCount = 0; receiversCount < receiversTotal; ++receiversCount)
                {
                    receivers[receiversCount] = senderSection.Receivers[receiversCount].Address;
                }

                senderConfiguration.To = receivers;

                return senderConfiguration;
            }
            catch (NullReferenceException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }

        //*** change recivers 
        public void Update(SenderConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            try
            {
                var senderSection = GetSenderSection(_configuration);

                senderSection.Host.Address = configuration.Host;
                senderSection.Host.Port = configuration.Port;

                senderSection.Credentials.Login = configuration.SenderLogin;
                senderSection.Credentials.Password = configuration.SenderPassword;

                _configuration.Save();
            }
            catch (NullReferenceException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }

        //*** NOT USED
        public SenderConfiguration Get(string name)
        {
            throw new System.NotImplementedException();
        }

        //*** NOT USED
        public void Set(SenderConfiguration entity)
        {
            throw new NotImplementedException();
        }

        #endregion IConfigurationManager<SenderConfiguration>



        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        private SenderSectionConfiguration GetSenderSection(System.Configuration.Configuration configuration)
        {
            var senderSection =
                configuration.Sections["senderConfiguration"] as SenderSectionConfiguration;

            return senderSection;
        }
    }
}
