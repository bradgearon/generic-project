using System;

namespace GenericProject.Core.Services
{
    public interface ILogger
    {
        void Info(string currentUser, string component, string message, params object[] args);

        void Debug(string currentUser, string component, string message, params object[] args);

        void Error(string currentUser, string component, string message, params object[] args);

        void Error(string currentUser, string component, Exception e, string message, params object[] args);
    }
}