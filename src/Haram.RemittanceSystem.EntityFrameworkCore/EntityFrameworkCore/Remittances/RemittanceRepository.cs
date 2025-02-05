﻿using Haram.RemittanceSystem.Remittances;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Haram.RemittanceSystem.EntityFrameworkCore.Remittances
{
    public class RemittanceRepository : EfCoreRepository<RemittanceSystemDbContext, Remittance, Guid>, IRemittanceRepository
    {
        public RemittanceRepository(IDbContextProvider<RemittanceSystemDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
        public async  Task<IQueryable<Remittance>> GetRemittancesWithData()
        {
            return  DbContext.Remittances.Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.IssuedBy)
                .Include(x => x.ApprovedBy)
                .Include(x => x.ReleasedBy)
                .Include(x=>x.currency)
                ;
        }


    }
}
