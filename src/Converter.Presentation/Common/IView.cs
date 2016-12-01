using System;

namespace Escyug.Converter.Presentation.Common
{
    /// <summary>
    ///     Base interface for all views
    /// </summary>
    public interface IView
    {
        /// <summary>
        ///     Rises event when view is initialized
        /// </summary>
        event Action InitializeView;

        /// <summary>
        ///     Shows view
        /// </summary>
        void Show();

        /// <summary>
        ///     Close view
        /// </summary>
        void Close();

        /// <summary>
        ///     Set error message
        /// </summary>
        string Error { set; }

        /// <summary>
        ///     Set notify message
        /// </summary>
        string Notify { set; }

        /// <summary>
        ///     Set warning message
        /// </summary>
        string Warning { set; }
    }
}
