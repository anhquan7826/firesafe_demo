using Application.DTOs;
using AutoMapper;
using Firesafe.Domain.Entities;

namespace Application.AutoMapper;

public class EntityToDtoMappingProfile : Profile
{
    public EntityToDtoMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Supplier, SupplierDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductShortDto>();
        CreateMap<Role, UserRoleDto>();
        CreateMap<Supplier, SupplierDto>();
        CreateMap<Supplier, SupplierShortDto>();
        CreateMap<Province, ProvinceDto>();
        CreateMap<Newspaper, NewspaperShortDto>();
        CreateMap<Newspaper, NewspaperDto>();
    }
}