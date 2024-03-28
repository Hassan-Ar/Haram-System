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
        //public Task<List<RemittanceDto>> GetAllRemittances(PagedAndSortedResultRequestDto input);
        //public Task<List<RemittanceDto>> GetAllDraftRemittances(PagedAndSortedResultRequestDto input);
        //public Task<List<RemittanceDto>> GetAllReadyRemittances(PagedAndSortedResultRequestDto input);
        //public Task<List<RemittanceDto>> GetAllApprovedRemittances(PagedAndSortedResultRequestDto input);
        //public Task<List<RemittanceDto>> GetAllReleasedRemittances(PagedAndSortedResultRequestDto input);
        //public Task<List<RemittanceDto>> GetAllUserRemittancesByUserId(Guid userId);
        //public Task<RemittanceDto> GetRemittanceByID(Guid remittanceId);
        //public Task<RemittanceDto> CreateRemittance(CreateUpdateRemittanceDto input);
        //public Task<RemittanceDto> UpdateRemittance(CreateUpdateRemittanceDto input);
        //public Task DeleteRemittance(Guid remittanceId);
        //public Task<RemittanceDto> SetAsReady(Guid remittanceID, Guid userID);
        //public Task<RemittanceDto> SetAsApproved(Guid remittanceID, Guid userID);
        //public Task<RemittanceDto> SetAsRelesad(Guid remittanceID, Guid userID, Guid receiverID);
    }
}
