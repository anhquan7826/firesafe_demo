using Application.AutoMapper;

namespace Firesafe.Service.StartupExtension;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class MapperExtension
{
    public static void AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(EntityToDtoMappingProfile),
            typeof(RequestModelToDtoMappingProfile),
            typeof(DtoToEntityMappingProfile),
            typeof(CommandToEntityMappingProfile),
            typeof(RequestModelToCommandMappingProfile));
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member