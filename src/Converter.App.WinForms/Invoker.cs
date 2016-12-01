using System;
using System.Threading.Tasks;

namespace Escyug.Converter.App.WinForms
{
    /// <summary>
    ///     Invokes events
    /// </summary>
    internal static class Invoker
    {
        /// <summary>
        ///     Invokes sync event (Action)
        /// </summary>
        /// <param name="action">Invoked event</param>
        public static void Invoke(Action action)
        {
            if (action != null)
            {
                action.Invoke();
            }
        }

        /// <summary>
        ///     Invokes async event (Task)
        /// </summary>
        /// <param name="func">Invoked event</param>
        /// <returns>Task</returns>
        public static async Task InvokeAsync(Func<Task> func)
        {
            if (func != null)
            {
                await func.Invoke();
            }
        }
    }
}
