using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Haram.RemittanceSystem.Statuses
{
    public interface IStatusAppService : ICrudAppService<
        StatusDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateStatusDto>
    {

    }
}
