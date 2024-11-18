using System;
using System.Collections.Generic;
using System.Text;

namespace LogLib
{
    public static class Log
    {
        public static void Debug(string message, Exception exception = null)
        => Logger.Instance.Debug(message, exception);

        public static void Info(string message, Exception exception = null)
            => Logger.Instance.Info(message, exception);

        public static void Warning(string message, Exception exception = null)
            => Logger.Instance.Warning(message, exception);

        public static void Error(string message, Exception exception = null)
            => Logger.Instance.Error(message, exception);

        public static void Fatal(string message, Exception exception = null)
            => Logger.Instance.Fatal(message, exception);
    }
}
