using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Haram.RemittanceSystem.Currencies
{
    public interface ICurrencyAppService : ICrudAppService<
        CurrencyDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCurrencyDto>
    {
         


    }
}
