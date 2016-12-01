using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escyug.Converter.Presentation.Common
{
    /// <summary>
    ///     Base presenter interface
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        ///     Launches presenter
        /// </summary>
        void Run();
    }

    /// <summary>
    ///     Base presenter interface for all presenters with one parameter
    /// </summary>
    /// <typeparam name="TArg"></typeparam>
    public interface IPresenter<in TArg>
    {
        /// <summary>
        ///     Launches presenter with argument of type TArgs
        /// </summary>
        /// <param name="argument"></param>
        void Run(TArg argument);
    }

}
