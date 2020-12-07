using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LoggerActivity
{
    internal static class Extension
    {
        public static string GetStackTrace(this MethodBase methodBase)
        {
            return methodBase.ReflectedType.Namespace + "." + methodBase.Name;
        }
    }
}
