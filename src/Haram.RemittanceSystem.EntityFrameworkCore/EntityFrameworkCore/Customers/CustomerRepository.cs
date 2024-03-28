using Haram.RemittanceSystem.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Haram.RemittanceSystem.EntityFrameworkCore.Customers
{
    public class CustomerRepository : EfCoreRepository<RemittanceSystemDbContext, Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(IDbContextProvider<RemittanceSystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
