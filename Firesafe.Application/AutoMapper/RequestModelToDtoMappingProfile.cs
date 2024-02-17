using Application.DTOs;
using Application.ViewModels.Requests.AddProduct;
using AutoMapper;

namespace Application.AutoMapper;

public class RequestModelToDtoMappingProfile : Profile
{
    public RequestModelToDtoMappingProfile()
    {
        CreateMap<AddProductRequest, ProductDto>();
    }
}