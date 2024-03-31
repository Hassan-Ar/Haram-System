using AutoMapper;
using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.Remittances;
using Haram.RemittanceSystem.Statuses;

namespace Haram.RemittanceSystem.Blazor;

public class RemittanceSystemBlazorAutoMapperProfile : Profile
{
    public RemittanceSystemBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<RemittanceDto, CreateUpdateRemittanceDto>().ReverseMap();
        CreateMap<CurrencyDto, CreateUpdateCurrencyDto>().ReverseMap();
        CreateMap<CustomerDto, CreateUpdateCustomerDto>().ReverseMap();
        CreateMap<StatusDto, CreateUpdateStatusDto>().ReverseMap();

    }
}
