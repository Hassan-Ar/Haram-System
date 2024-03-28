using Haram.RemittanceSystem.StatusTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Haram.RemittanceSystem.Statuses
{
    public class CreateUpdateStatusDto 
    {
        [Required]
        public Guid? Id { get; set; }
        [Required]
        public DateTime StatusDate { get; set; }
        [Required]
        public StatusType statusType { get; set; }
        public Guid remittanceId { get; set; }
    }
}
