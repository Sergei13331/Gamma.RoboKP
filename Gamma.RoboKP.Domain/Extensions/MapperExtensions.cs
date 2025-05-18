using System.Reflection;
using Gamma.RoboKP.Domain.MapperRegister;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Gamma.RoboKP.Domain.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection RegisterMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        var registers = config.Scan(Assembly.GetAssembly(typeof(RequestModelsRegister)));
        config.Apply(registers);
        services.AddSingleton(config);
        services.AddScoped<IMapper, Mapper>();
        return services;
    }
}