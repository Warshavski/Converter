using System;
using System.Linq.Expressions;

namespace Escyug.Converter.Presentation.Common
{
    /// <summary>
    ///     Application controller interface, which is contained within IoC - container
    /// </summary>
    public interface IApplicationController
    {
        // some kind of builder pattern    

        /// <summary>
        ///     (IoC) Register view and its implementation. 
        /// </summary>
        /// <typeparam name="TView">View interface.</typeparam>
        /// <typeparam name="TImplementation">Implementation of TView interface.</typeparam>
        /// <returns></returns>
        IApplicationController RegisterView<TView, TImplementation>()
            where TView : IView
            where TImplementation : class, TView;

        /// <summary>
        ///     (IoC) Register service and its implementation.
        /// </summary>
        /// <typeparam name="TService">Service interface(abstract class).</typeparam>
        /// <typeparam name="TImplementation">Implementation of TService.</typeparam>
        /// <returns></returns>
        IApplicationController RegisterService<TService, TImplementation>()
            where TImplementation : class, TService;

        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TArgument"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        IApplicationController RegisterInstance<TArgument>(TArgument instance);
        

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TArgument"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        IApplicationController RegisterInstance<TService, TArgument>(TService instance);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPresenter"></typeparam>
        void Run<TPresenter>()
            where TPresenter : class, IPresenter;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPresenter"></typeparam>
        /// <typeparam name="TArgument"></typeparam>
        /// <param name="argument"></param>
        void Run<TPresenter, TArgument>(TArgument argument)
             where TPresenter : class, IPresenter<TArgument>;
    }
}
