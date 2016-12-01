namespace Escyug.Converter.Models.Entities
{
    public class ConverterTask
    {
        // CONSTANTS SECTION
        //---------------------------------------------------------------------

        #region ConverterTask constants

        /// <summary>
        ///     Default converter scheduled task name
        /// </summary>
        public const string DefaultTaskName = "Processing center converter";

        /// <summary>
        ///     Default converte scheduled task argument
        /// </summary>
        public const string DefaultTaskArguments = "-noui";

        /// <summary>
        ///     Default converter scheduled task trigger time(hours)
        /// </summary>
        public const short DefaultTriggerHours = 10;

        /// <summary>
        ///     Default converte scheduled task trigger time(minutes)
        /// </summary>
        public const short DefaultTriggerMinutes = 20;

        #endregion ConverterTask constants



        // PROPERTIES SECTION
        //---------------------------------------------------------------------

        #region ConverterTask properties

        /// <summary>
        ///     Name of the scheduled task
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Hour of day trigger will fire
        /// </summary>
        public short Hours { get; set; }

        /// <summary>
        ///     Minutes of hour trigger will fire
        /// </summary>
        public short Minutes { get; set; }

        /// <summary>
        ///     Command-line parameters
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        ///     The application file path that task to run
        /// </summary>
        public string Path { get; set; }

        #endregion ConverterTask properties



        // CONSTRUCTORS SECTION
        //---------------------------------------------------------------------

        #region ConverterTask constructors

        /// <summary>
        ///     Initialize converter task by default values
        /// </summary>
        public ConverterTask()
        {
            Name = DefaultTaskName;
            Parameters = DefaultTaskArguments;
            Hours = DefaultTriggerHours;
            Minutes = DefaultTriggerMinutes;
        }
        
        /// <summary>
        ///     Initialize converter task
        /// </summary>
        /// <param name="name">Name of the task </param>
        /// <param name="hours">Hours of the trigger that will fire every day</param>
        /// <param name="minutes">Minutes of the trigger that will fire every day</param>
        public ConverterTask(string name, short hours, short minutes)
        {
            Name = name;
            Hours = hours;
            Minutes = minutes;
        }

        /// <summary>
        ///     Initialize converter task
        /// </summary>
        /// <param name="name">Name of the task </param>
        /// <param name="arguments">Command line paramaters for application</param>
        /// <param name="hours">Hours of the trigger that will fire every day</param>
        /// <param name="minutes">Minutes of the trigger that will fire every day</param>
        public ConverterTask(string name, string arguments, short hours, short minutes)
            : this(name, hours, minutes)
        {
            Parameters = arguments;
        }

        /// <summary>
        ///     Initialize converter task
        /// </summary>
        /// <param name="name">Name of the task </param>
        /// <param name="arguments">Command line paramaters for application</param>
        /// <param name="appPath">Path to the application that task run</param>
        /// <param name="hours">Hours of the trigger that will fire every day</param>
        /// <param name="minutes">Minutes of the trigger that will fire every day</param>
        public ConverterTask(string name, string arguments, string appPath, short hours, short minutes)
            : this(name, arguments, hours, minutes)
        {
            Path = appPath;
        }

        #endregion ConverterTask constructors
    }
}
