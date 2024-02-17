using Application.ViewModels.Requests.AddProduct;
using AutoMapper;
using Firesafe.Domain.Commands;

namespace Application.AutoMapper;

public class RequestModelToCommandMappingProfile : Profile
{
    public RequestModelToCommandMappingProfile()
    {
        CreateMap<AddProductRequest, AddNewProductCommand>();
    }
}