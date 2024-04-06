using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Haram.RemittanceSystem.Remittances
{
    public interface IRemittanceAppServices : ICrudAppService<RemittanceDto,Guid, GetListRemittancesInputDto, CreateUpdateRemittanceDto>
    {
        //Changing the Statuse Of Remittance
        public Task<RemittanceDto> ChangeStatus(Guid id, Guid? receiverId = null);
    }
}
