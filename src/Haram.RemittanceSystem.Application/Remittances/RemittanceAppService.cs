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
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Haram.RemittanceSystem.Remittances
{

    public class RemittanceAppService : CrudAppService<Remittance, RemittanceDto, Guid, GetListRemittancesInputDto, CreateUpdateRemittanceDto>, IRemittanceAppServices
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IRepository<IdentityUser> _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICurrentUser _user;
        private readonly IIdentityRoleRepository _roleRepository;
        private readonly IRemittanceRepository _remittancerepository;
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="customerRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="currencyRepository"></param>
        /// <param name="user"></param>
        /// <param name="remittancerepository"></param>
        /// <param name="roleRepository"></param>
        public RemittanceAppService(IRepository<Remittance, Guid> repository, ICustomerRepository customerRepository, IRepository<IdentityUser> userRepository, ICurrencyRepository currencyRepository, ICurrentUser user, IRemittanceRepository remittancerepository, IIdentityRoleRepository roleRepository) : base(repository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _user = user;
            _remittancerepository = remittancerepository;
            GetPolicyName = RemittanceSystemPermissions.Remittances.Default;
            GetListPolicyName = RemittanceSystemPermissions.Remittances.Default;
            CreatePolicyName = RemittanceSystemPermissions.Remittances.Create;
            UpdatePolicyName = RemittanceSystemPermissions.Remittances.Edit;
            DeletePolicyName = RemittanceSystemPermissions.Remittances.Delete;
            _roleRepository = roleRepository;
            // CreatePolicyName = RemittanceSystemPermissions.Remittances.Create;
        }

        /// <summary>
        /// Create Remittance
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<RemittanceDto> CreateAsync(CreateUpdateRemittanceDto input)
        {
            var sender = (await _customerRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == input.SenderId);
            //check for receiver 
            if (sender is null)
            {
                throw new UserFriendlyException("Remittance sender is not found ");
            }

            var currency = (await _currencyRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == input.CurrencyID);
            // Currency and Remittance Type Validation 
            if ((input.Type == RemittanceTypes.RemittanceType.Internal && currency.AlphabeticCode != "SYP") || (input.Type == RemittanceTypes.RemittanceType.External && currency.AlphabeticCode == "SYP"))
            {
                throw new UserFriendlyException(" You Can only use SYP Currency With Internal Remittance , Internal Remittance only with SYP currency");
            }

            var user = (await _userRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == _user.Id);
            //check for User 
            if (user is null)
            {
                throw new UserFriendlyException("user not found");
            }
            var remittance = MapToEntity(input);
            remittance.IssuedBy = user;
            remittance.Sender = sender;
            remittance.SetAmmount(input.Amount);
            await Repository.InsertAsync(remittance);
            return MapToGetOutputDto(remittance);
        }

        /// <summary>
        /// Update Remittance
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<RemittanceDto> UpdateAsync(Guid id, CreateUpdateRemittanceDto input)
        {

            var remittance = (await Repository.GetQueryableAsync()).FirstOrDefault(x => x.Id == id);
           
            //check for remittance 
            if (remittance is null)
            {
                throw new UserFriendlyException("Remittance not found");
            }
            // Type Validation
            if (remittance.Status != StatusType.Issued)
            {
                //TODO:add localization
                throw new UserFriendlyException(L[$"Remittence in{remittance.Status} status could not be updated  "]);
            }
           // remittance = MapToEntity(input);
           remittance.SerialNo = input.SerialNo;
            remittance.SetAmmount(input.Amount);
            remittance.Type = input.Type;
            remittance.CurrencyID = input.CurrencyID;
            remittance.SenderId = input.SenderId;
            await Repository.UpdateAsync(remittance);

            return MapToGetOutputDto(remittance);
        }


        /// <summary>
        /// To get async Query of Remittances With Filters
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected async override Task<IQueryable<Remittance>> CreateFilteredQueryAsync(GetListRemittancesInputDto input)
        {
            var x = (await _remittancerepository.GetRemittancesWithData())
                .WhereIf(input.Type.HasValue, x => x.Type == input.Type)
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                .WhereIf(!string.IsNullOrEmpty(input.name), x => x.Sender.FirstName.Contains(input.name))
                .PageBy(input.SkipCount, input.MaxResultCount);

            return x;
        }
        
        /// <summary>
        /// Changing the Statuse Of Remittance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        //TODO: Change the roles
        public async Task<RemittanceDto> ChangeStatus(Guid id, Guid? receiverId = null)
        {

            var remittance = (await Repository.GetQueryableAsync()).FirstOrDefault(x => x.Id == id);
            //Check for null remittance 
            if (remittance is null)
            {
                throw new UserFriendlyException(L["Remittance not found"]);
            }
            var userId = _user.Id;
            var user = (await _userRepository.WithDetailsAsync()).FirstOrDefault(x => x.Id == userId);
            //Check for user
            if (user is null)
            {
                throw new UserFriendlyException(L["An authorized User"]);
            }
            //Check for  the last status if it was Issued(draft) 
            if (remittance.Status == StatusType.Issued)
            {
                SetReady(remittance);
                remittance.IssuedBy = user;
            }
            //Check for  the last status if it was Ready
            else if (remittance.Status == StatusType.Ready)
            {
                //Check for the requierd Role
                SetApproved(remittance);
                remittance.ApprovedBy = user;
            }
            //Check for  the last status if it was Approved
            else if (remittance.Status == StatusType.Approved)
            {
                var receiver = (await _customerRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == receiverId);
                //Check for the Receiver Role
                if (receiver is null)
                {
                    throw new UserFriendlyException(L["Remittance receiver not found"]);
                }
                //Check for the Receiver to be not the sender
                if (receiver.Id == remittance.SenderId)
                {
                    throw new UserFriendlyException(L["Remittance receiver not Be the Sender"]);
                }

                remittance.Receiver = receiver;
                SetReleased(remittance);
                remittance.ReleasedBy = user;
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
        /// Delet Remittance
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

        /// <summary>
        /// method to set the status the remittance Ready 
        /// </summary>
        /// <param name="remittance"></param>
        [Authorize(Roles = "Admin")]
        private void SetReady(Remittance remittance)
        {

            remittance.ChangeStatus();

        }

        /// <summary>
        /// method to set the status the remittance approved 
        /// </summary>
        /// <param name="remittance"></param>
        [Authorize(Roles = "Admin")]
        private void SetApproved(Remittance remittance)
        {

            remittance.ChangeStatus();

        }

        /// <summary>
        /// method to set the status of the remittance Released 
        /// </summary>
        /// <param name="remittance"></param>
        [Authorize(Roles = "Admin")]
        private void SetReleased(Remittance remittance)
        {

            remittance.ChangeStatus();

        }

    }
}

