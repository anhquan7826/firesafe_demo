using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Firesafe.Domain.Commands;
using Firesafe.Domain.Entities;

namespace Application.AutoMapper;

public class CommandToEntityMappingProfile : Profile
{
    public CommandToEntityMappingProfile()
    {
        CreateMap<AddNewProductCommand, Product>()
            .ForMember(x => x.Categories, options => options.Ignore());
    }
}