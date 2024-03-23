using Haram.RemittanceSystem.AppUser;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.RemittanceTypes;
using Haram.RemittanceSystem.Statuses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Haram.RemittanceSystem.Remittances
{
    public class CreateUpdateRemittanceDto
    {
        [Required]
        [MaxLength(100)]
        public string SerialNo { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        [Required]
        public RemittanceType Type { get; set; }
        public Guid? ApprovedByID { get; set; }
        [Required]
        public Guid? ReleasedById { get; set; }
        public Guid? IssuedById { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public Guid ReceiverId { get; set; }
        [Required]
        public Guid CurrencyID { get; set; }
    }
}
