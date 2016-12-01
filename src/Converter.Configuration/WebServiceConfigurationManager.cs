using System;
using System.Configuration;
using System.ServiceModel.Configuration;

using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories.Exceptions;
using System.ServiceModel;

namespace Escyug.Converter.Configuration
{
    public class WebServiceConfigurationManager : IConfigurationManager<WebServiceConfiguration>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly System.Configuration.Configuration _configuration;
        

        
        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public WebServiceConfigurationManager(System.Configuration.Configuration configuration)
        {
            _configuration = configuration;    
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IConfiguration<WebServiceConfiguration>

        //*** NOT USED
        public WebServiceConfiguration Get()
        {
            throw new NotImplementedException();
        }

        public WebServiceConfiguration Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            try
            {
                var clientSection = GetClientSection();

                var endpoint = SearchEndPoint(clientSection, name);
                if (endpoint == null)
                {
                    return null;
                }

                var webServiceConfig = new WebServiceConfiguration()
                {
                    Name = endpoint.Name,
                    RemoteAddress = endpoint.Address.AbsoluteUri
                };

                return webServiceConfig;
            }
            catch (NullReferenceException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }

        //*** NOT USED
        public void Set(WebServiceConfiguration entity)
        {
            throw new NotImplementedException();
        }

        public void Update(WebServiceConfiguration entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                /**
                var clientSection = GetClientSection();

                var endpoint = SearchEndPoint(clientSection, entity.Name);
                if (endpoint == null)
                {
                    throw new InvalidOperationException(nameof(endpoint));
                }

                var endpointAddress = new Endpoi

                endpoint.Address = uri;
                */

                var serviceModeGroup = 
                    ServiceModelSectionGroup.GetSectionGroup(_configuration);

                if (serviceModeGroup == null)
                {
                    throw  new RepositoryLoadException(
                        "WebService configuration update : Can't load configuration section");
                }

                ChannelEndpointElement existingElement = 
                    SearchEndPoint(serviceModeGroup.Client, entity.Name);

                /**
                ChannelEndpointElement existingElement =   //null;
                foreach (ChannelEndpointElement endpoint in serviceModeGroup.Client.Endpoints)
                {
                    if (endpoint.Name == entity.Name)
                    {
                        existingElement = endpoint;
                        break;
                    }
                }
                */

                EndpointAddress endpointAddress = new EndpointAddress(entity.RemoteAddress);
                var newElement = new ChannelEndpointElement(endpointAddress, existingElement.Contract)
                {
                    BehaviorConfiguration = existingElement.BehaviorConfiguration,
                    Binding = existingElement.Binding,
                    BindingConfiguration = existingElement.BindingConfiguration,
                    Name = existingElement.Name
                    // Set other values
                };

                serviceModeGroup.Client.Endpoints.Remove(existingElement);
                serviceModeGroup.Client.Endpoints.Add(newElement);
                
                ConfigurationManager.RefreshSection("system.serviceModel/client");
                _configuration.Save();
            }
            catch (NullReferenceException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }

        #endregion IConfiguration<WebServiceConfiguration>



        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        private ClientSection GetClientSection()
        {
            var clientSection =
                System.Configuration.ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;

            return clientSection;
        }

        private ChannelEndpointElement SearchEndPoint(ClientSection clientSection, string name)
        {
            var clientEndPoints = clientSection.Endpoints;

            if (clientEndPoints == null || clientEndPoints.Count == 0)
            {
                return null;
            }
            /** LINQ variant of the foreach block below
             * return clientEndPoints.Cast<ChannelEndpointElement>().FirstOrDefault(endpoint => string.Equals(name, endpoint.Name));
             */
            foreach (ChannelEndpointElement endpoint in clientEndPoints)
            {
                if (string.Equals(name, endpoint.Name))
                {
                    return endpoint;
                }
            }

            return null;
        }
    }
}
