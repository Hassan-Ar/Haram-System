using Haram.RemittanceSystem.Remittances;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Haram.RemittanceSystem.Customers
{
    public class CustomerAppService : CrudAppService<
        Customer,
        CustomerDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCustomerDto>,
        ICustomerAppService
    {
        private readonly IRepository<Remittance> _remittanceRepository;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="remittanceRepository"></param>
        public CustomerAppService(IRepository<Customer, Guid> repository, IRepository<Remittance> remittanceRepository) : base(repository)
        {
            _remittanceRepository = remittanceRepository;
        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UserFriendlyException"></exception>
        public async override Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            // Check if the input is valid according to data annotations
            if (!Validator.TryValidateObject(input, new ValidationContext(input), null, true))
            {
                throw new UserFriendlyException("Invalid input. Please make sure all required fields are provided.");
            }
            //check for the uniqueness of the required Names
            if (await Repository.FirstOrDefaultAsync(p =>
                p.FirstName == input.FirstName &&
                p.LastName == input.LastName &&
                p.FatherName == input.FatherName &&
                p.MotherName == input.MotherName) != null)
            {
                throw new UserFriendlyException(L["A Customer with the same combination of names already exists."]);
            }
            var customer = MapToEntity(input);
            await Repository.InsertAsync(customer);
            return MapToGetOutputDto(customer);
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async override Task DeleteAsync(Guid id)
        {
            var customer = await Repository.GetAsync(id);
            var remittance = await _remittanceRepository.AnyAsync(x => x.SenderId == id || x.ReceiverId == id);
            // Check if the customer has any associated remittances
            if (remittance )
            {
                throw new UserFriendlyException(L["Cannotdeletethecustomerbecaustheyhaveassociatedremittances"]);
            }

            // If the customer has no associated remittances, proceed with deletion
            await Repository.DeleteAsync(customer);
        }
    }
}
