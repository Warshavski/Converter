using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;
using Ninject.Modules;

using log4net.Config;

using Escyug.Converter.Common.Logging;

using Escyug.Converter.Data.Ado;
using Escyug.Converter.Data.Ado.Repositories;

using Escyug.Converter.Models.Entities;
using Escyug.Converter.Models.Repositories;
using Escyug.Converter.Models.Utils.MailSender;
using Escyug.Converter.Models.Utils.XmlConverter;
using Escyug.Converter.Models.Services;
using System.Collections.Specialized;


namespace Escyug.Converter.App.Console
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            ConfigureLog4Net();
            ConfigureDbContext();


            // REPOSITORIES CONFIGURE SECTION
            //-----------------------------------------------------------------
            Bind<IRepository<RemainRow>>().To<RemainRepository>();
            Bind<IRepository<RecipeRow>>().To<RecipeRepository>();


            // UTILS CONFIGURE SECTION
            Bind<IXmlConverter<RemainRow>>().To<RemainXmlConverter>();
            //ConfigureMailSender();


            // SERVICES CONFIGURE SECTION
            //-----------------------------------------------------------------
            Bind<EntityService<RemainRow>>().To<RemainService>();
            Bind<EntityService<RecipeRow>>().To<RecipeService>();

            Bind<IGuideService>().To<GuideService>();
        }

        private void ConfigureLog4Net()
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();
            Bind<ILogManager>().ToConstant(logManager);
        }

       

        private void ConfigureDbContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RemainsTest"].ConnectionString;
            var providerName = ConfigurationManager.ConnectionStrings["RemainsTest"].ProviderName;

            var dbContext = new DbContext(connectionString, providerName);
            Bind<DbContext>().ToConstant(dbContext);
        }
    }
}
