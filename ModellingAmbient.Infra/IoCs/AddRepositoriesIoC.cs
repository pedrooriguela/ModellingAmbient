using Microsoft.Extensions.DependencyInjection;
using ModellingAmbient.Core.Interfaces;
using ModellingAmbient.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModellingAmbient.Infra.IoCs;

public static class AddRepositoriesIoC
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IB3Repository, B3Repository>();

        return services;

    }
}
