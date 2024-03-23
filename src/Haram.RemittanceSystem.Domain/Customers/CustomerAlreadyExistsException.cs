using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Haram.RemittanceSystem.Customers
{
    public class CustomerAlreadyExistsException : BusinessException
    {
        public CustomerAlreadyExistsException(string name)
     : base(RemittanceSystemDomainErrorCodes.CustomerAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
