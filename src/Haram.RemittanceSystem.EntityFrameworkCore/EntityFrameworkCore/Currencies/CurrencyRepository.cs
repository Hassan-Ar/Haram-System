using Haram.RemittanceSystem.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Haram.RemittanceSystem.EntityFrameworkCore.Currencies
{
    public class CurrencyRepository : EfCoreRepository<RemittanceSystemDbContext, Currency, Guid>, ICurrencyRepository
    {
        public CurrencyRepository(IDbContextProvider<RemittanceSystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

    }
}
