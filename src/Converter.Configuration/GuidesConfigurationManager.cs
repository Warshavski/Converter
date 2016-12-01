using System;
using System.Collections.Generic;

using Escyug.Converter.Models.Entities.Configurations;
using Escyug.Converter.Models.Repositories;

using Escyug.Converter.Configuration.Sections;
using Escyug.Converter.Models.Repositories.Exceptions;

namespace Escyug.Converter.Configuration
{
    public class GuidesConfigurationManager : IConfigurationManager<GuidesConfiguration>
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        private readonly System.Configuration.Configuration _configuration;
        


        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        public GuidesConfigurationManager(System.Configuration.Configuration configuration)
        {
            _configuration = configuration;    
        }



        // PUBLIC INTERFACE METHODS SECTION
        //---------------------------------------------------------------------

        #region IConfigurationManager<GuidesConfiguration>

        public GuidesConfiguration Get()
        {
            try
            {
                var guidesSection = GetGuidesSection(_configuration);

                var rootFolder = AppDomain.CurrentDomain.BaseDirectory;
                var guiderPath = guidesSection.Path.Value.Replace("{AppDir}", rootFolder);
                var guidesLastUpdateDate = guidesSection.LastUpdateDate.Value;

                var guidesConfiguration = new GuidesConfiguration
                {
                    LastUpdateDate = guidesLastUpdateDate,
                    Path = guiderPath
                };

                var guidesTotal = guidesSection.Guides.Count;
                var guides = new Dictionary<string, string[]>();
                for (int guidesCount = 0; guidesCount < guidesTotal; ++guidesCount)
                {
                    var guideName = guidesSection.Guides[guidesCount].Name;
                    var guideFile = guidesSection.Guides[guidesCount].File;
                    var guideUpdateDate = guidesSection.Guides[guidesCount].Date;

                    guides.Add(guideName, new string[] {guideFile, guideUpdateDate});
                }

                guidesConfiguration.Guides = guides;

                return guidesConfiguration;
            }
            catch (NullReferenceException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }

        //*** NOT USED
        public GuidesConfiguration Get(string name)
        {
            throw new NotImplementedException();
        }

        //*** NOT USED
        public void Set(GuidesConfiguration entity)
        {
            throw new NotImplementedException();
        }

        public void Update(GuidesConfiguration configuration)
        {
            if (configuration == null)
            {
                throw  new ArgumentNullException(nameof(configuration));
            }
            
            try
            {
                var guidesSection = GetGuidesSection(_configuration);

                guidesSection.Path.Value = configuration.Path;
                guidesSection.LastUpdateDate.Value = configuration.LastUpdateDate;

                var guidesTotal = guidesSection.Guides.Count;
                for (int guidesCount = 0; guidesCount < guidesTotal; ++guidesCount)
                {
                    var guideName = guidesSection.Guides[guidesCount].Name;

                    guidesSection.Guides[guidesCount].File = configuration.Guides[guideName][0];
                    guidesSection.Guides[guidesCount].Date = configuration.Guides[guideName][1];
                }

                _configuration.Save();
            }
            catch (NullReferenceException ex)
            {
                throw new RepositoryLoadException(ex.Message, ex);
            }
        }

        #endregion IConfigurationManager<GuidesConfiguration>



        // PRIVATE HELPER METHODS SECTION
        //---------------------------------------------------------------------

        private GuidesSectionConfiguration GetGuidesSection(System.Configuration.Configuration configuration)
        {
            var guidesSection = 
                configuration.Sections["guidesConfiguration"] as GuidesSectionConfiguration;

            return guidesSection;
        }
    }
}
