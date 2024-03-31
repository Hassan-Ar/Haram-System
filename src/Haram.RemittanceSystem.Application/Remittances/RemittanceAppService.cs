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
        private readonly IRemittanceRepository _remittancerepository;

        public RemittanceAppService(IRepository<Remittance, Guid> repository, ICustomerRepository customerRepository, IRepository<IdentityUser> userRepository, ICurrencyRepository currencyRepository, ICurrentUser user, IRemittanceRepository remittancerepository) : base(repository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _user = user;
            _remittancerepository = remittancerepository;
            // CreatePolicyName = RemittanceSystemPermissions.Remittances.Create;
        }

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
            
            var user = (await _userRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == _user.Id);
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

        //TODO: rewrite 
        protected async override Task<IQueryable<Remittance>> CreateFilteredQueryAsync(GetListRemittancesInputDto input)
        {
            var x =( await _remittancerepository.GetRemittancesWithData())
                .WhereIf(input.Type.HasValue, x => x.Type == input.Type)
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
                .PageBy(input.SkipCount, input.MaxResultCount);
            //var result = (await Repository.WithDetailsAsync())
            //                        .WhereIf(input.Type.HasValue, x => x.Type == input.Type)
            //                        .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
            //                        .PageBy(input.SkipCount, input.MaxResultCount);
            return x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        //TODO: Change the roles
        public async Task<RemittanceDto> ChangeStatus(Guid id , Guid? receiverId = null)
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
                //if (_user.Roles.Contains("User1"))
                //{
                    remittance.ChangeStatus(StatusType.Ready);
                //}
                //else
                //{
                //    throw new UserFriendlyException(L["User dose not have the Permission to change the status"]);
                //}
            }
            //Check for  the last status if it was Ready
            else if (remittance.Status == StatusType.Ready)
            {
                //Check for the requierd Role
                //if (_user.Roles.Contains("User2"))
                //{
                    remittance.ChangeStatus(StatusType.Approved);
                //}
                //else
                //{
                //    throw new UserFriendlyException(L["User dose not have the Permission to change the status"]);
                //}
            }
            //Check for  the last status if it was Approved
            else if (remittance.Status == StatusType.Approved)
            {
                //Check for the requierd Role
                //if (_user.Roles.Contains("User3"))
                //{
                    var receiver = (await _customerRepository.GetQueryableAsync()).FirstOrDefault(x => x.Id == receiverId);
                    if (receiver is null)
                    {
                        throw new UserFriendlyException(L["Remittance receiver not found"]);
                    }
                    remittance.Receiver = receiver;
                    remittance.ChangeStatus(StatusType.Released);
                //}
                //else
                //{
                //    throw new UserFriendlyException(L["User dose not have the Permission to change the status"]);
                //}
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

