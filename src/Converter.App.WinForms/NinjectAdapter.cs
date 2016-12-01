using System;

using Ninject;

using Escyug.Converter.Presentation.Common;

namespace Escyug.Converter.App.WinForms
{
    class NinjectAdapter : IContainer, IDisposable
    {
        private readonly StandardKernel _kernel = new StandardKernel(); 

        public void Register<TService, TImplementation>() 
            where TImplementation : TService
        {
            _kernel.Bind<TService>().To<TImplementation>();
        }

        public void Register<TService>()
        {
            _kernel.Bind<TService>().ToSelf();
        }

        public void RegisterInstance<T>(T instance)
        {
            _kernel.Bind<T>().ToConstant(instance);
        }
        
        public TService Resolve<TService>()
        {
            return _kernel.Get<TService>();
        }

        public bool IsRegistered<TService>()
        {
            return _kernel.CanResolve<TService>();
        }
        
        public void RegisterInstance<TService, TArgument>(TService instance)
        {
            _kernel.Bind<TService>().ToConstant(instance).WhenInjectedInto(typeof(TArgument));
        }


        public void Dispose()
        {
            _kernel?.Dispose();
        }
    }
}
