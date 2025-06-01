using System.Reflection;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Identity;
using Gamma.RoboKP.Infrastructure.MapperInfToDomainRegister;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Gamma.RoboKP.Infrastructure.Extensions;

public static class MapperInfToDomainExtension
{
    public static IServiceCollection RegisterMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        var registers = config.Scan(Assembly.GetAssembly(typeof(InfrastructureToDomainRegister))!);
        config.Apply(registers);
        services.AddSingleton(config);
        services.AddScoped<IMapper, Mapper>();
        TypeAdapterConfig<User, AppUser>.NewConfig()
            .Map(dest => dest.Company, src => src.Company);

        return services;
    }
}