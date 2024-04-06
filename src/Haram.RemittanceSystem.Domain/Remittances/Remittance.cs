using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.RemittanceTypes;
using Haram.RemittanceSystem.Statuses;
using Haram.RemittanceSystem.StatusTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace Haram.RemittanceSystem.Remittances
{
    public class Remittance : FullAuditedAggregateRoot<Guid>
    {
        public string SerialNo { get; set; }
        public double Amount { get; set; }
        public double TotalAmount { get; set; }
        public RemittanceType Type { get; set; }
        public StatusType Status { get; set; }

        public string? ReceiverFirstName { get; set; }
        public string? ReceiverLirstName { get; set; }
        public string? ReceiverPhone { get; set; }

        #region navigation
        public Guid? ApprovedByID { get; set; }

        [ForeignKey(nameof(ApprovedByID))]
        public virtual IdentityUser? ApprovedBy { get; set; }

        public Guid? ReleasedById { get; set; }

        [ForeignKey(nameof(ReleasedById))]
        public virtual IdentityUser? ReleasedBy { get; set; }

        public Guid? IssuedById { get; set; }

        [ForeignKey(nameof(IssuedById))]
        public virtual IdentityUser? IssuedBy { get; set; }

        public Guid SenderId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public virtual Customer Sender { get; set; }

        public Guid? ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public virtual Customer? Receiver { get; set; }
        public virtual ICollection<Status> Statuses { get; set; } = new List<Status>();
        public Guid CurrencyID { get; set; }

        [ForeignKey(nameof(CurrencyID))]
        public virtual Currency currency { get; set; }

        #endregion


        public void SetAmmount(double ammount)
        {
            this.Amount = ammount;
            TotalAmount = Amount + ((Amount * 5) / 100);
        }

        public void ChangeStatus()
        {
            if (this.Status == StatusType.Issued)
            {
                this.Status = StatusType.Ready;
                this.Statuses.Add(new Status
                {
                    Type = StatusType.Ready
                });
            }
            else if (this.Status == StatusType.Ready)
            {
                this.Status = StatusType.Approved;
                this.Statuses.Add(new Status
                {
                    Type = StatusType.Approved
                });
            }
            else if (this.Status == StatusType.Approved)
            {
                this.Status = StatusType.Released;
                this.Statuses.Add(new Status
                {
                    Type = StatusType.Released
                });
            }

        }




    }
}
