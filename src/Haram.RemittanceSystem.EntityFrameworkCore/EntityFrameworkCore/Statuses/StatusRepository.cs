using Haram.RemittanceSystem.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Haram.RemittanceSystem.EntityFrameworkCore.Statuses
{
    public class StatusRepository : EfCoreRepository<RemittanceSystemDbContext, Status, Guid>, IStatusRepository
    {
        public StatusRepository(IDbContextProvider<RemittanceSystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
