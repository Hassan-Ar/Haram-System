﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Haram.RemittanceSystem.Currencies
{
    public  interface ICurrencyRepository : IRepository<Currency,Guid>
    {
        public Task<Currency> GetwithoutPaged();
    }
}
