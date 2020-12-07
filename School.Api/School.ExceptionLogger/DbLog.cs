using System;
using System.Collections.Generic;
using System.Text;

namespace School.ExceptionLogger
{
    public sealed class DbLogger<T> where T : new()
    {
        private static readonly object padlock = new object();
        private static T _instance = default(T);
        DbLogger() { }

        public static IDb Log
        {
            get
            {
                return (IDb)GetInstance();
            }
        }

        private static IDb GetInstance()  
        {
            if (_instance == null)
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }

            return _instance as IDb;
        }

    }

}
