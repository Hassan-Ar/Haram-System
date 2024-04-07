using Haram.RemittanceSystem.AppUser;
using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.RemittanceTypes;
using Haram.RemittanceSystem.Statuses;
using Haram.RemittanceSystem.StatusTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Haram.RemittanceSystem.Remittances
{
    public class RemittanceDto : AuditedEntityDto<Guid>
    {
        public string SerialNo { get; set; }
        public double Amount { get; set; }
        public double TotalAmount { get; set; }
        public RemittanceType Type { get; set; }
        public StatusType Status { get; set; }
        public Guid? ApprovedByID { get; set; }
        public  IdentityUserDto? ApprovedBy { get; set; }
        public Guid? ReleasedById { get; set; }
        public IdentityUserDto? ReleasedBy { get; set; }
        public Guid? IssuedById { get; set; }
        public IdentityUserDto? IssuedBy { get; set; }
        public Guid SenderId { get; set; }
        public  CustomerDto Sender { get; set; }
        public Guid? ReceiverId { get; set; }
        public  CustomerDto? Receiver { get; set; }
        public  IList<StatusDto> Statuses { get; set; }
        public Guid CurrencyID { get; set; }
        public  CurrencyDto currency { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLirstName { get; set; }
        public string ReceiverPhone { get; set; }


    }
}
