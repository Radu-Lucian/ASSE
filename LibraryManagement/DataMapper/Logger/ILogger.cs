// <copyright file="ILogger.cs" company="Transilvania University Of Brasov">
// Radu Lucian Andrei
// </copyright>
// <summary> Base interface for Logging. </summary>
namespace DataMapper.Logger
{
    using System.Reflection;

    /// <summary>
    /// ILogger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        void LogError(string message, MethodBase method);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogError(string message);

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        void LogInfo(string message, MethodBase method);

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        void LogWarning(string message, MethodBase method);

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogWarning(string message);
    }
}
