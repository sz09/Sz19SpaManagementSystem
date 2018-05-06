
namespace Infrastructure.Logging
{
    using log4net;
    using System.Runtime.CompilerServices;

    public static class LoggingExtension
    {
        #region Attributes
        private static readonly ILog logger = LogManager.GetLogger(typeof(LoggingExtension));
        #endregion

        #region Function
        
        /// <summary>
        /// Extension method
        /// Function log after entered method with method name
        /// </summary>
        /// <param name="logger">[this] param</param>
        /// <param name="methodName">name of method called</param>
        public static void EnterMethod(this ILog logger, [CallerMemberName] string methodName = "") 
        {
            logger.Debug("Enter method [" + methodName + "]");
        }

        /// <summary>
        /// Extension method
        /// Function log before leaving method with method name
        /// </summary>
        /// <param name="logger">[this] param</param>
        /// <param name="MethodName">name of method called</param>
        public static void LeaveMethod(this ILog logger, [CallerMemberName] string methodName = "")
        {
            logger.Debug("Leave method [" + methodName + "]");
        }

        #endregion
    }
}
