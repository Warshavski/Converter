using System;
using System.Linq.Expressions;

namespace Escyug.Converter.Presentation.Common
{
    public sealed class ApplicationController : IApplicationController
    {
        private readonly IContainer _container;

        public ApplicationController(IContainer container)
        {
            _container = container;
            _container.RegisterInstance<IApplicationController>(this);
        }



        // IOC CONTAINER SECTION
        //---------------------------------------------------------------------

        public IApplicationController RegisterView<TView, TImplementation>()
            where TView : IView
            where TImplementation : class, TView
        {
            _container.Register<TView, TImplementation>();
            return this;
        }

        public IApplicationController RegisterInstance<TInstance>(TInstance instance)
        {
            _container.RegisterInstance(instance);
            return this;
        }
        
        public IApplicationController RegisterService<TModel, TImplementation>()
            where TImplementation : class, TModel
        {
            _container.Register<TModel, TImplementation>();
            return this;
        }

        public IApplicationController RegisterInstance<TService, TArgument>(TService instance)
        {
            _container.RegisterInstance<TService, TArgument>(instance);
            return this;
        }



        // PRESENTER WORKFLOW SECTION
        //---------------------------------------------------------------------

        public void Run<TPresenter>()
            where TPresenter : class, IPresenter
        {
            if (!_container.IsRegistered<TPresenter>())
            {
                _container.Register<TPresenter>();
            }

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run();
        }

        public void Run<TPresenter, TArgument>(TArgument argument)
            where TPresenter : class, IPresenter<TArgument>
        {
            if (!_container.IsRegistered<TPresenter>())
            {
                _container.Register<TPresenter>();
            }

            var presenter = _container.Resolve<TPresenter>();
            presenter.Run(argument);
        }
    }
}
