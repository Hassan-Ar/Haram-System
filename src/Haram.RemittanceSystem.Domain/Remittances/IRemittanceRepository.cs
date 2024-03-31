using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Haram.RemittanceSystem.Remittances
{
    public interface IRemittanceRepository : IRepository<Remittance,Guid>
    {

        public Task<IQueryable<Remittance>> GetRemittancesWithData();

    }
}
