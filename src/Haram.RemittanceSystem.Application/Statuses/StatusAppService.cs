using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Haram.RemittanceSystem.Statuses
{
    public class StatusAppService : CrudAppService<Status, StatusDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStatusDto>, IStatusAppService
    {
        public StatusAppService(IRepository<Status, Guid> repository) : base(repository)
        {

        }
    }
}
