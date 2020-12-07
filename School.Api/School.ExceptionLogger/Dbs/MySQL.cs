using System;
using System.Collections.Generic;
using System.Text;

namespace School.ExceptionLogger
{
    public sealed class MySQL : IDb
    {
        
        public string ConnectionStringName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Debug(string Debug)
        {
            throw new NotImplementedException();
        }

        public void Error(string Error)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string Fatal)
        {
            throw new NotImplementedException();
        }

        public void Info(string Info)
        {
            throw new NotImplementedException();
        }

        public void Trace(string Trace)
        {
            throw new NotImplementedException();
        }

        public void Warning(string Warning)
        {
            throw new NotImplementedException();
        }
    }
}
