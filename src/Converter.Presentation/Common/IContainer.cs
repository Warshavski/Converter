using System;
using System.Linq.Expressions;

namespace Escyug.Converter.Presentation.Common
{
    /// <summary>
    ///     Interface for IoC - container (Adapter)
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        ///     Register service
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        void Register<TService, TImplementation>()
            where TImplementation : TService;

        void Register<TService>();

        void RegisterInstance<T>(T instance);

        TService Resolve<TService>();

        bool IsRegistered<TService>();
        
        void RegisterInstance<TService, TArgument>(TService instance);
    }
}
