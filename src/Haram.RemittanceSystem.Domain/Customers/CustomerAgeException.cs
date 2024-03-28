using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Haram.RemittanceSystem.Customers
{
    public class CustomerAgeException : BusinessException
    {

        public CustomerAgeException(string name)
: base(RemittanceSystemDomainErrorCodes.CustomerAgeMustBeOver18)
        {
         
        }
    }
}
