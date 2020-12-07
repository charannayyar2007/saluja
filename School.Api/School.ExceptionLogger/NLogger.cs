
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;


namespace School.ExceptionLogger
{
    public sealed class NLogger : ILogger
    {

        private static readonly object padlock = new object();
        private static NLogger _instance = null;
        private static Logger _logger = null;

        public string FullFilePathName { get; set; }
        public static NLogger Log
        {
            get => GetInstance();
        }

        private NLogger()
        {
        }

        private static NLogger GetInstance()
        {
            if (_instance == null)
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new NLogger();
                    }
                }
            }
            return _instance;
        }
          
        private static Logger GetLogger(string MethodName)
        {
            if (_logger == null)
            {
                 
                _logger = LogManager.GetLogger(MethodName);
            }
            return _logger;
        }

        public void Debug(string Debug)
        {
            try
            {

                var methodBase = new StackTrace().GetFrame(1).GetMethod();
                GetLogger(GetStackTrace(methodBase)).Debug(Debug);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Error(string Error)
        {
            try
            {
                var methodBase = new StackTrace().GetFrame(1).GetMethod();
                GetLogger(GetStackTrace(methodBase)).Error(Error);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Fatal(string Fatal)
        {
            try
            {
                var methodBase = new StackTrace().GetFrame(1).GetMethod();
                GetLogger(GetStackTrace(methodBase)).Fatal(Fatal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public new void Info(string Info)
        {
            try
            {
                var methodBase = new StackTrace().GetFrame(1).GetMethod();
                GetLogger(GetStackTrace(methodBase)).Info(Info);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Warning(string Warning)
        {
            try
            {
                var methodBase = new StackTrace().GetFrame(1).GetMethod();
                GetLogger(GetStackTrace(methodBase)).Warn(Warning);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Trace(string Trace)
        {
            try
            {
                var methodBase = new StackTrace().GetFrame(1).GetMethod();
                GetLogger(GetStackTrace(methodBase)).Trace(Trace);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetStackTrace(MethodBase methodBase)
        {
            return methodBase.ReflectedType.Namespace + "." + methodBase.Name;
        }
    }
}
