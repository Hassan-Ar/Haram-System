using Haram.RemittanceSystem.Remittances;
using Haram.RemittanceSystem.StatusTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Haram.RemittanceSystem.Statuses
{
    public class Status : AuditedEntity<Guid>
    {
        public StatusType Type { get; set; }

        [ForeignKey(nameof(Remittance))]
        public Guid RemittanceId { get; set; }
        public virtual Remittance remittance { get; set; }
    }
}
