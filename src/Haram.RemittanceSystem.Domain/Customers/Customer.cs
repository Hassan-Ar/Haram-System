using Haram.RemittanceSystem.Genders;
using Haram.RemittanceSystem.Remittances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Haram.RemittanceSystem.Customers
{
    public class Customer : FullAuditedAggregateRoot<Guid>
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public Gender CustGender { get; set; }

        #region navigitionProperties
        public virtual ICollection<Remittance>? SendedRemittances { get; set; }
        public virtual ICollection<Remittance>? ReceivedRemittances { get; set; }
        #endregion




    }
}
