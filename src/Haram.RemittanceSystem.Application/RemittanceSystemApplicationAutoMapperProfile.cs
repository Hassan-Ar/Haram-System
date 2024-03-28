using AutoMapper;
using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.Remittances;
using Haram.RemittanceSystem.Statuses;

namespace Haram.RemittanceSystem;

public class RemittanceSystemApplicationAutoMapperProfile : Profile
{
    public RemittanceSystemApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Customer,CustomerDto>().ReverseMap();
        CreateMap<Customer, CreateUpdateCustomerDto>().ReverseMap();

        CreateMap<Remittance, RemittanceDto>().ReverseMap();
        CreateMap<Remittance, CreateUpdateRemittanceDto>().ReverseMap();

        CreateMap<Status, StatusDto>().ReverseMap();
        CreateMap<Status, CreateUpdateStatusDto>().ReverseMap();

        CreateMap<Currency, CurrencyDto>().ReverseMap();
        CreateMap<Currency, CreateUpdateCurrencyDto>().ReverseMap();
    }
}
