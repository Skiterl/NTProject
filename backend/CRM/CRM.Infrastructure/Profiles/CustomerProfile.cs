using AutoMapper;
using CRM.Domain.Aggregates.UserAggregate;
using CRM.Infrastructure.Dtos;

namespace CRM.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
        }
    }
}
