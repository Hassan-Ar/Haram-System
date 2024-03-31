using Haram.RemittanceSystem.Remittances;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Haram.RemittanceSystem.Currencies
{
    public class CurrencyAppService : CrudAppService<Currency, CurrencyDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCurrencyDto>, ICurrencyAppService
    {
        private readonly IRepository<Remittance> _remittanceRepository;

        public CurrencyAppService(IRepository<Currency, Guid> repository, IRepository<Remittance> remittanceRepository) : base(repository)
        {
            _remittanceRepository = remittanceRepository;
        }
        public async override Task<CurrencyDto> CreateAsync(CreateUpdateCurrencyDto input)
        {
            // Check if the input is valid according to data annotations
            if (!Validator.TryValidateObject(input, new ValidationContext(input), null, true))
            {
                throw new ArgumentException("Invalid input. Please make sure all required fields are provided.");
            }
            //check for the uniqueness of the required Names
            if (await Repository.FirstOrDefaultAsync(p =>
                p.Name == input.Name &&
                p.AlphabeticCode == input.AlphabeticCode) != null)
            {
                throw new ArgumentException("A Customer with the same combination of names already exists.");
            }

            var currency = MapToEntity(input);
            await Repository.InsertAsync(currency);
            return MapToGetOutputDto(currency);
        }

        public async override Task DeleteAsync(Guid id)
        {
            var currency = await Repository.GetAsync(id);
            var remittance = await _remittanceRepository.AnyAsync(x => x.currency.Id == id);
            // Check if the customer has any associated remittances
            if (remittance)
            {
                throw new ApplicationException("Cannot delete the currency because they have associated remittances.");
            }
            // If the customer has no associated remittances, proceed with deletion
            await Repository.DeleteAsync(currency);
        }
    }
}
