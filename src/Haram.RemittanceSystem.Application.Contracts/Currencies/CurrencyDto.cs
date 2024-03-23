using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.RemittanceSystem.Currencies
{
    public class CurrencyDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string SympolPngPath { get; set; }
    }
}
