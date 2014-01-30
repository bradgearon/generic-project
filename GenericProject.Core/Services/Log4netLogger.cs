using System;

namespace GenericProject.Core.Services
{
    public class Log4NetLogger : ILogger
    {
        public void Info(string currentUser, string component, string message, params object[] args) { GetLogger(component).Info(FormatMessage(currentUser, message, args)); }

        public void Debug(string currentUser, string component, string message, params object[] args) { GetLogger(component).Debug(FormatMessage(currentUser, message, args)); }

        public void Error(string currentUser, string component, string message, params object[] args) { GetLogger(component).Error(FormatMessage(currentUser, message, args)); }

        public void Error(string currentUser, string component, Exception e, string message, params object[] args) { GetLogger(component).Error(FormatMessage(currentUser, message, args), e); }

        private log4net.ILog GetLogger(string component)
        {
            if (string.IsNullOrEmpty(component))
                throw new InvalidOperationException("Cannot log without specifying a component.");

            return log4net.LogManager.GetLogger(component);
        }

        private object FormatMessage(string currentUser, string message, object[] args)
        {
            var msg = string.Format(message, args);
            return string.Concat(currentUser, ": ", msg);
        }
    }
}