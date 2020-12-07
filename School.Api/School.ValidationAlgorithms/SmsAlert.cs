using School.ValidationAlgorithms.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.ValidationAlgorithms
{
    public class SmsAlert : ISmsAlet
    {
        public virtual bool IsSmsSent(object type)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsSmsSent()
        {
            throw new NotImplementedException();
        }
    }
}
