using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace School.ExceptionLogger
{
    public sealed class MSSQLServer : IDb
    {
        
        public string ConnectionStringName { get; set; }

        public void Debug(string Debug)
        {
            var methodBase = new StackTrace().GetFrame(1).GetMethod();
            InsertIntoDatabase(methodBase.GetStackTrace(), Debug, MethodBase.GetCurrentMethod().Name);
        }

        public void Error(string Error)
        {
            var methodBase = new StackTrace().GetFrame(1).GetMethod();
            InsertIntoDatabase(methodBase.GetStackTrace(), Error, MethodBase.GetCurrentMethod().Name);
        }

        public void Fatal(string Fatal)
        {
            var methodBase = new StackTrace().GetFrame(1).GetMethod();
            InsertIntoDatabase(methodBase.GetStackTrace(), Fatal, MethodBase.GetCurrentMethod().Name);
        }

        public void Info(string Info)
        {
            var methodBase = new StackTrace().GetFrame(1).GetMethod();
            InsertIntoDatabase(methodBase.GetStackTrace(), Info, MethodBase.GetCurrentMethod().Name);
        }

        public void Trace(string Trace)
        {
            var methodBase = new StackTrace().GetFrame(1).GetMethod();
            InsertIntoDatabase(methodBase.GetStackTrace(), Trace, MethodBase.GetCurrentMethod().Name);
        }

        public void Warning(string Warning)
        {
            var methodBase = new StackTrace().GetFrame(1).GetMethod();
            InsertIntoDatabase(methodBase.GetStackTrace(), Warning, MethodBase.GetCurrentMethod().Name);
        }


        private void InsertIntoDatabase(string stackTrace, string errorMessage, string errorType = null)
        {
            SqlHelper.ExecuteNonQuery(this.ConnectionStringName, stackTrace, errorMessage, errorType);
        }
    }



}
