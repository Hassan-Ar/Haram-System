using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Haram.RemittanceSystem.Currencies
{
    public class CurrencyAppService : CrudAppService<Currency, CurrencyDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCurrencyDto>, ICurrencyAppService
    {
        public CurrencyAppService(IRepository<Currency, Guid> repository) : base(repository)
        {

        }
    }
}
