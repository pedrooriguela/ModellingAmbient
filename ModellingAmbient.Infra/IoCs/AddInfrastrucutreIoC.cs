using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModellingAmbient.Infra.IoCs;

public static class AddInfrastrucutreIoC
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.ConfigureSettings(config);
        services.AddServices();
        services.AddRepositories();

        return services;
    }
}
