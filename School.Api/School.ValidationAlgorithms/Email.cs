using School.ValidationAlgorithms.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.ValidationAlgorithms
{
    public class Email : IEmailAlert
    {
        public virtual bool IsEmailSent(object type)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsEmailSent()
        {
            throw new NotImplementedException();
        }
    }
}
