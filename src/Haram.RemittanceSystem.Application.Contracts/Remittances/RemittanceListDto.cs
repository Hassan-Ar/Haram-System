using Haram.RemittanceSystem.AppUser;
using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.RemittanceTypes;
using Haram.RemittanceSystem.Statuses;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.RemittanceSystem.Remittances
{
    public class RemittanceListDto : AuditedEntityDto<Guid>
    {
        public string SerialNo { get; set; }
        public double Amount { get; set; }
        public double TotalAmount { get; set; }
        public RemittanceType Type { get; set; }
        public Guid? ApprovedByID { get; set; }
        public UserDto? ApprovedBy { get; set; }
        public Guid? ReleasedById { get; set; }
        public UserDto? ReleasedBy { get; set; }
        public Guid? IssuedById { get; set; }
        public UserDto? IssuedBy { get; set; }
        public Guid SenderId { get; set; }
        public CustomerDto Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public CustomerDto Receiver { get; set; }
        public StatusDto Statuses { get; set; }
        public Guid CurrencyID { get; set; }
        public CurrencyDto currency { get; set; }

    }
}
