using Haram.RemittanceSystem.Remittances;
using Haram.RemittanceSystem.StatusTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.RemittanceSystem.Statuses
{
    public class StatusDto : AuditedEntityDto<Guid>
    {
        public StatusType statusType { get; set; }

        public Guid remittanceId { get; set; }
    }
}
