using Application.DTOs;
using AutoMapper;
using Firesafe.Domain.Entities;

namespace Application.AutoMapper;

public class DtoToEntityMappingProfile : Profile
{
    public DtoToEntityMappingProfile()
    {
        CreateMap<ProductDto, Product>();
    }
}