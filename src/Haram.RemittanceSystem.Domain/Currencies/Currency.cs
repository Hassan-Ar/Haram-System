using Haram.RemittanceSystem.Remittances;
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
        #region nav
        public virtual ICollection<Remittance>? remittances { get; set; }
        #endregion

        // default ctor
        protected Currency()
        {
            
        }
        // ctor
        public Currency(string name, string alphabeticCode)
        {
            Name = name;
            AlphabeticCode = alphabeticCode;
        }
        //create currency
        internal static Currency Create(string title, string code)
        {
         return new Currency(title,code);
        }
    }
}
