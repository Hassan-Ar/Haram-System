using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Haram.RemittanceSystem.Remittances
{
    public interface IRemittanceAppServices : ICrudAppService<
        RemittanceDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateRemittanceDto>
    {
    }
}
