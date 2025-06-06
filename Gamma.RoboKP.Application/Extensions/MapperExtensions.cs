using System.Reflection;
using Gamma.RoboKP.Application.MapperRegister;
using Gamma.RoboKP.Infrastructure.RepositoryMapping;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Gamma.RoboKP.Application.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection RegisterMapster(this IServiceCollection services)
    {
        var repoConfig = new TypeAdapterConfig();
        var repoRegisters = repoConfig.Scan(Assembly.GetAssembly(typeof(RepositoryMappings)));
        repoConfig.Apply(repoRegisters);
        
        var serviceConfig = new TypeAdapterConfig();
        var serviceRegister = serviceConfig.Scan(Assembly.GetAssembly(typeof(RequestModelsRegister)));
        repoConfig.Apply(serviceRegister);
        
        services.AddKeyedSingleton("RepositoryMappings", repoConfig);
        services.AddKeyedSingleton("ControllerMapperConfig", serviceConfig);

        services.AddKeyedScoped<IMapper>("RepositoryMapper",
            (sp, key) => new Mapper(sp.GetRequiredKeyedService<TypeAdapterConfig>("RepositoryMappings")));
        
        services.AddKeyedScoped<IMapper>("ControllerMapper",
            (sp, key) => new Mapper(sp.GetRequiredKeyedService<TypeAdapterConfig>("ControllerMapperConfig")));

        return services;
    }
}