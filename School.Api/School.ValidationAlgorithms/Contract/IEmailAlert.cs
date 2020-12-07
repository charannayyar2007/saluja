using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.ValidationAlgorithms.Contract
{
   public interface IEmailAlert
    {
        bool IsEmailSent(object type);

        bool IsEmailSent();
    }
}
