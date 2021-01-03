// <copyright file="Logger.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Base interface for Logging. </summary>
namespace DataMapper.Logger
{
    using System.Reflection;

    /// <summary>
    /// Logger class.
    /// </summary>
    /// <seealso cref="DataMapper.Logger.ILogger" />
    public class Logger : ILogger
    {
        /// <summary>
        /// The log object.
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        public void LogError(string message, MethodBase method)
        {
            string methodName = method.DeclaringType.Name + "." + method.Name;
            if (Log.IsInfoEnabled)
            {
                Log.Error("[" + methodName + "]:" + message);
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogError(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        public void LogInfo(string message, MethodBase method)
        {
            string methodName = method.DeclaringType.Name + "." + method.Name;
            if (Log.IsInfoEnabled)
            {
                Log.Info("[" + methodName + "]:" + message);
            }
        }

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogInfo(string message)
        {
            Log.Info(message);
        }

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        public void LogWarning(string message, MethodBase method)
        {
            string methodName = method.DeclaringType.Name + "." + method.Name;
            if (Log.IsInfoEnabled)
            {
                Log.Warn("[" + methodName + "]:" + message);
            }
        }

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogWarning(string message)
        {
            Log.Warn(message);
        }
    }
}
