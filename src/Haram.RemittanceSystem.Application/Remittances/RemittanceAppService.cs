using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.Statuses;
using Haram.RemittanceSystem.StatusTypes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Haram.RemittanceSystem.Permissions;
using Volo.Abp.Users;

namespace Haram.RemittanceSystem.Remittances
{
    public class RemittanceAppService : CrudAppService<Remittance, RemittanceDto, Guid, GetListRemittancesInputDto, CreateUpdateRemittanceDto>, IRemittanceAppServices
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IRepository<IdentityUser> _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICurrentUser _user;



        public RemittanceAppService(IRepository<Remittance, Guid> repository, ICustomerRepository customerRepository, IRepository<IdentityUser> userRepository, ICurrencyRepository currencyRepository, ICurrentUser user) : base(repository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _user = user;
            // CreatePolicyName = RemittanceSystemPermissions.Remittances.Create;
        }
        #region Hassasn Code
        // public async Task<RemittanceDto> CreateRemittance(CreateUpdateRemittanceDto input)
        // {
        //     // Check for null input
        //     if (input == null) throw new ArgumentNullException(nameof(input));
        //     // Check for invalid amount input
        //     if (input.Amount <= 0)
        //     {
        //         throw new UserFriendlyException("Cannot Create Remittance Without a pose Ammount");
        //     }

        //     try
        //     {
        //         var sender = (await _customerRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == input.SenderId);
        //         //ceck for receiver 
        //         if (sender is null)
        //         {
        //             throw new UserFriendlyException("Remittance sender is not found ");
        //         }
        //         var Currency = (await _currencyRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == input.CurrencyID);
        //         //Check for matching the remittance type with the currency type 
        //         if ((input.Type == RemittanceTypes.RemittanceType.Internal && Currency.AlphabeticCode != "SYP") || (input.Type == RemittanceTypes.RemittanceType.External && Currency.AlphabeticCode == "SYP"))
        //         {
        //             throw new UserFriendlyException(" You Can only use SYP Currency With Internal Remittance , Internal Remittance only with SYP currency");
        //         }
        //         var user = (await _userRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == input.IssuedById);
        //         //Check for user 
        //         if (user is null)
        //         {
        //             throw new UserFriendlyException(" user not found");
        //         }
        //         var remittance = ObjectMapper.Map<CreateUpdateRemittanceDto,Remittance>(input);
        //         remittance.IssuedBy = user;
        //         remittance.Sender = sender;
        //         remittance.TotalAmount = input.Amount + ((input.Amount * 5) / 100);
        //         remittance.Statuse = new Status() { statusType = StatusType.Issued };
        //         var result = await _remittanceRepository.InsertAsync(remittance);
        //         return ObjectMapper.Map<Remittance, RemittanceDto>(result);

        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }

        // }

        // public async Task DeleteRemittance(Guid remittanceId)
        // {
        //     try
        //     {
        //         var remittance = (await _remittanceRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == remittanceId);
        //         //Check for null input
        //         if (remittance == null)
        //         {
        //             throw new UserFriendlyException(L["Remittace not found to be deleted "]);
        //         }
        //         //Check for remittance status
        //         if (remittance.Statuse.statusType != StatusType.Issued)
        //         {
        //             throw new UserFriendlyException(L["remmitance could not be deleted because its status is not Issued"]);
        //         }
        //         await _remittanceRepository.DeleteAsync(remittanceId);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }

        //     throw new NotImplementedException();
        // }

        // public async Task<List<RemittanceDto>> GetAllApprovedRemittances(PagedAndSortedResultRequestDto input)
        // {
        //     try
        //     {
        //         var Remittances = await _remittanceRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
        //         var result = Remittances.Where(x => x.Statuse.statusType == StatusType.Approved).ToList();
        //         return ObjectMapper.Map<List<Remittance>, List<RemittanceDto>>(result);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        // public async Task<List<RemittanceDto>> GetAllDraftRemittances(PagedAndSortedResultRequestDto input)
        // {
        //     try
        //     {
        //         var Remittances = await _remittanceRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
        //         var result = Remittances.Where(x => x.Statuse.statusType == StatusType.Issued).ToList();
        //         return ObjectMapper.Map<List<Remittance>, List<RemittanceDto>>(result);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        // public async Task<List<RemittanceDto>> GetAllReadyRemittances(PagedAndSortedResultRequestDto input)
        // {
        //     try
        //     {
        //         var Remittances = await _remittanceRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
        //         var result = Remittances.Where(x => x.Statuse.statusType == StatusType.Ready).ToList();
        //         return ObjectMapper.Map<List<Remittance>, List<RemittanceDto>>(result);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        // public async Task<List<RemittanceDto>> GetAllReleasedRemittances(PagedAndSortedResultRequestDto input)
        // {
        //     try
        //     {
        //         var Remittances = await _remittanceRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
        //         var result = Remittances.Where(x => x.Statuse.statusType == StatusType.Released).ToList();
        //         return ObjectMapper.Map<List<Remittance>, List<RemittanceDto>>(result);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        // public async Task<List<RemittanceDto>> GetAllRemittances(PagedAndSortedResultRequestDto input)
        // {
        //     try
        //     {
        //         var Remittances = await _remittanceRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting);
        //         return ObjectMapper.Map<List<Remittance>, List<RemittanceDto>>(Remittances);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        //// TODO-:implement the Repository getMethod for this endpoint
        // public async Task<List<RemittanceDto>> GetAllUserRemittancesByUserId(Guid userId)
        // {
        //     try
        //     {
        //         //var Remittances = await _remittanceRepository;
        //         //return ObjectMapper.Map<List<Remittance>, List<RemittanceDto>>(Remittances);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        //     throw new NotImplementedException();

        // }

        // public async Task<RemittanceDto> GetRemittanceByID(Guid remittanceId)
        // {
        //     try
        //     {
        //         var remittance = (await _remittanceRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == remittanceId);
        //         return ObjectMapper.Map<Remittance, RemittanceDto>(remittance);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        // public async Task<RemittanceDto> SetAsApproved(Guid remittanceID, Guid userID)
        // {
        //     try
        //     {
        //         var remittance = (await _remittanceRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == remittanceID);
        //         //check for remittance to be found
        //         if (remittance is null)
        //         {
        //             throw new UserFriendlyException("Remittance not found to be approved");
        //         }
        //         //ceck for remittance statuse to be ready before
        //         if (remittance.Statuse.statusType != StatusType.Ready)
        //         {
        //             throw new UserFriendlyException("Remittance is not ready to be set as Approved ");
        //         }
        //         remittance.Statuse.statusType = StatusType.Approved;
        //         remi
        //         remittance.ApprovedByID = userID;
        //         await _remittanceRepository.UpdateAsync(remittance);
        //         return ObjectMapper.Map<Remittance, RemittanceDto>(remittance);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }
        //  rem.status = new status (){}
        // public async Task<RemittanceDto> SetAsReady(Guid remittanceID, Guid userID)
        // {
        //     try
        //     {
        //         var remittance = (await _remittanceRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == remittanceID);
        //         //check for remittance to be found
        //         if (remittance is null)
        //         {
        //             throw new UserFriendlyException("Remittance not found to be ready");
        //         }
        //         //ceck for remittance statuse to be Draft before
        //         if (remittance.Statuse.statusType != StatusType.Issued)
        //         {
        //             throw new UserFriendlyException("Remittance is not Draft  to be set as ready ");
        //         }
        //         remittance.Statuse.statusType = StatusType.Ready;
        //         remittance.IssuedById = userID;
        //         await _remittanceRepository.UpdateAsync(remittance);
        //         return ObjectMapper.Map<Remittance, RemittanceDto>(remittance);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        // public async Task<RemittanceDto> SetAsRelesad(Guid remittanceID, Guid userID, Guid receiverID)
        // {
        //     try
        //     {
        //         var remittance = (await _remittanceRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == remittanceID);
        //         //check for remittance to be found
        //         if (remittance is null)
        //         {
        //             throw new UserFriendlyException("Remittance not found to be released");
        //         }
        //         //ceck for remittance statuse to be Approved before
        //         if (remittance.Statuse.statusType != StatusType.Approved)
        //         {
        //             throw new UserFriendlyException("Remittance is not approved  to be set as released ");
        //         }
        //         var receiver = (await _customerRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == receiverID);
        //         //ceck for receiver 
        //         if (receiver is null)
        //         {
        //             throw new UserFriendlyException("Remittance receiver is not found ");
        //         }
        //         remittance.Receiver = receiver;
        //         remittance.Statuse.statusType = StatusType.Released;
        //         remittance.ApprovedByID = userID;
        //         await _remittanceRepository.UpdateAsync(remittance);
        //         return ObjectMapper.Map<Remittance, RemittanceDto>(remittance);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }

        // public async Task<RemittanceDto> UpdateRemittance(CreateUpdateRemittanceDto input)
        // {
        //     try
        //     {
        //         var remittance = (await _remittanceRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == input.Id);
        //         if (remittance is null)
        //         {
        //             throw new UserFriendlyException("Remittance not found");
        //         }
        //         remittance = ObjectMapper.Map<CreateUpdateRemittanceDto, Remittance>(input);
        //         await _remittanceRepository.UpdateAsync(remittance);

        //         return ObjectMapper.Map<Remittance, RemittanceDto>(remittance);
        //     }
        //     catch (Exception)
        //     {

        //         throw;
        //     }
        // }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<RemittanceDto> CreateAsync(CreateUpdateRemittanceDto input)
        {
            var sender = (await _customerRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == input.SenderId);
            //ceck for receiver 
            if (sender is null)
            {
                throw new UserFriendlyException("Remittance sender is not found ");
            }

            var currency = (await _currencyRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == input.CurrencyID);
            if ((input.Type == RemittanceTypes.RemittanceType.Internal && currency.AlphabeticCode != "SYP") || (input.Type == RemittanceTypes.RemittanceType.External && currency.AlphabeticCode == "SYP"))
            {
                throw new UserFriendlyException(" You Can only use SYP Currency With Internal Remittance , Internal Remittance only with SYP currency");
            }

            var user = (await _userRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == input.IssuedById);
            if (user is null)
            {
                throw new UserFriendlyException("user not found");
            }
            var remittance = MapToEntity(input);
            remittance.IssuedBy = user;
            remittance.Sender = sender;
            remittance.SetAmmount(input.Amount);
            remittance.ChangeStatus(StatusType.Issued);

            await Repository.InsertAsync(remittance);

            return MapToGetOutputDto(remittance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<RemittanceDto> UpdateAsync(Guid id, CreateUpdateRemittanceDto input)
        {

            var remittance = (await Repository.GetQueryableAsync()).FirstOrDefault(x => x.Id == id);
            if (remittance is null)
            {
                throw new UserFriendlyException("Remittance not found");
            }
            if (remittance.Status != StatusType.Issued)
            {
                //TODO:add localization
                throw new UserFriendlyException(L[""]);
            }

            remittance = MapToEntity(input);
            await Repository.UpdateAsync(remittance);

            return MapToGetOutputDto(remittance);
        }


        protected async override Task<IQueryable<Remittance>> CreateFilteredQueryAsync(GetListRemittancesInputDto input)
        {

            var result = (await Repository.GetQueryableAsync())
                                    .WhereIf(input.Type.HasValue, x => x.Type == input.Type)
                                    .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                                    .PageBy(input.SkipCount, input.MaxResultCount);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        //TODO: Change the roles
        public async Task<RemittanceDto> ChangeStatus(Guid id)
        {

            var remittance = (await Repository.GetQueryableAsync()).FirstOrDefault(x => x.Id == id);
            //Check for null remit id
            if (remittance is null)
            {
                throw new UserFriendlyException(L["Remittance not found"]);
            }

            var userId = _user.Id;
            var user = (await _userRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == userId);

            //Check for  the last status if it was Issued(draft) 
            if (remittance.Status == StatusType.Issued)
            {
                //Check for the requierd Role
                if (_user.Roles.Contains("Admin"))
                {
                    remittance.ChangeStatus(StatusType.Ready);
                }
                else
                {
                    throw new UserFriendlyException(L["User dose not have the Permission to change the status"]);
                }
            }
            //Check for  the last status if it was Ready
            else if (remittance.Status == StatusType.Ready)
            {
                //Check for the requierd Role
                if (_user.Roles.Contains("Admin"))
                {
                    remittance.ChangeStatus(StatusType.Approved);
                }
                else
                {
                    throw new UserFriendlyException(L["User dose not have the Permission to change the status"]);
                }
            }
            //Check for  the last status if it was Approved
            else if (remittance.Status == StatusType.Approved)
            {
                //Check for the requierd Role
                if (_user.Roles.Contains("Admin"))
                {
                    remittance.ChangeStatus(StatusType.Released);
                }
                else
                {
                    throw new UserFriendlyException(L["User dose not have the Permission to change the status"]);
                }
            }
            //Check for  the last status if it was Released
            else if (remittance.Status == StatusType.Released)
            {
                throw new UserFriendlyException(L["Remittance was released before"]);

            }

            await Repository.UpdateAsync(remittance);

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        protected async override Task DeleteByIdAsync(Guid id)
        {
            var remittance = (await Repository.GetQueryableAsync()).FirstOrDefault(x => x.Id == id);
            //Check for Remittance  
            if (remittance is null)
            {
                throw new UserFriendlyException("Remittance is not found to be deleted ");
            }
            //Check for the Remittance Type if it's Remittance
            if (remittance.Status != StatusType.Issued)
            {
                throw new UserFriendlyException("Only Draft Remittances could be deleted ");
            }
            await base.DeleteByIdAsync(id);
            return;
        }

    }
}

