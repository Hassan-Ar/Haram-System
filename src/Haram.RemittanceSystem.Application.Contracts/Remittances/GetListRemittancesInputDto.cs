using Haram.RemittanceSystem.RemittanceTypes;
using Haram.RemittanceSystem.StatusTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.RemittanceSystem.Remittances
{
    public class GetListRemittancesInputDto : PagedAndSortedResultRequestDto
    {
        public RemittanceType? Type { get; set; }
        public StatusType? Status { get; set; }
    }
}
