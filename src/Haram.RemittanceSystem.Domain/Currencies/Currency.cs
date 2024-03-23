using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Haram.RemittanceSystem.Currencies
{
    public class Currency : AuditedEntity<Guid>
    {
        public string Name { get;private set; }
        public string AlphabeticCode { get;private set; }

        protected Currency()
        {
            
        }

        public Currency(string name, string alphabeticCode)
        {
            Name = name;
            AlphabeticCode = alphabeticCode;
        }
        internal static Currency Create(string title, string code)
        {
         return new Currency(title,code);
        }
    }
}
