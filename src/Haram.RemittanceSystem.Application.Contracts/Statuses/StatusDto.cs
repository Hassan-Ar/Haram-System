using Haram.RemittanceSystem.Remittances;
using Haram.RemittanceSystem.StatusTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haram.RemittanceSystem.Statuses
{
    public class StatusDto
    {
        public DateTime StatusDate { get; set; }
        public StatusType statusType { get; set; }

        public Guid remittanceId { get; set; }
        public virtual RemittanceDto remittance { get; set; }
    }
}
