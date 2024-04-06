using Haram.RemittanceSystem.AppUser;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.RemittanceTypes;
using Haram.RemittanceSystem.Statuses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.RemittanceSystem.Remittances
{ 
    public class CreateUpdateRemittanceDto : EntityDto
    {
        public Guid? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string SerialNo { get; set; }
        [Required]
        [Range(1,5000000)]
        public double Amount { get; set; }
        [Required]
        public RemittanceType Type { get; set; }
        public Guid? IssuedById { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public Guid CurrencyID { get; set; }
        public Guid? ReciverId { get; set; }
        public double? TotalAmount { get; set; }
    }
}
