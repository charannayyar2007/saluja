using System;

namespace LoggerActivity
{
    public interface ILogger
    {
        void Info(string Info);
        void Error(string Error);
        void Debug(string Debug);
        void Warning(string Warning);
        void Fatal(string Fatal);
        void Trace(string Trace);
    }
}
