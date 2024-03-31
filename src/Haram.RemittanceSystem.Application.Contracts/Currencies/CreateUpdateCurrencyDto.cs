using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.RemittanceSystem.Currencies
{
    public class CreateUpdateCurrencyDto 
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string AlphabeticCode { get; set; }
    }
}
