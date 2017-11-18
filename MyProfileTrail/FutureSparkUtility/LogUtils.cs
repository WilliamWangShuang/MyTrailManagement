using LogLibrary;
using System;

namespace MyProfileTrail.FutureSparkUtility
{
    public class LogUtils
    {
        private const string ErrorCategory = "CustomError";
        private const string InfoCatetofy = "CustomInfo";
        private const string DebugCategory = "CustomDebug";
        private static LogHelper logHelper = new LogHelper();

        private static void Write(string message, string category)
        {
            logHelper.Write(message, category);
        }
        
        /// <summary>
        /// Write info to the log
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            logHelper.Write(message, InfoCatetofy);
        }

        /// <summary>
        /// Write debug message to the log
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            logHelper.Write(message, DebugCategory);
        }
        
        /// <summary>
        /// Write the error message to the log
        /// </summary>
        /// <param name="message"></param>
        public static void Error( string message)
        {
            logHelper.Write(message, ErrorCategory);
        }

        public static void Error(string message, Exception e)
        {
            var errorMessage = BuildErrorMsg(message, e);
            logHelper.Write(errorMessage, ErrorCategory);
        }

        private static string BuildErrorMsg(string message, Exception exception)
        {
            string errorMsg = message;
            errorMsg += Environment.NewLine;
            if (exception != null)
            {
                errorMsg += exception.Message;
                errorMsg += Environment.NewLine;
                errorMsg += exception.StackTrace;
                if (exception.InnerException != null)
                {
                    errorMsg += "InnerException:" + Environment.NewLine;
                    errorMsg += BuildErrorMsg(string.Empty, exception.InnerException);
                }

            }
            return errorMsg;
        }

    }
}
