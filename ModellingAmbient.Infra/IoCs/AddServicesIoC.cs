using Microsoft.Extensions.DependencyInjection;
using ModellingAmbient.Infra.Interfaces;
using ModellingAmbient.Infra.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModellingAmbient.Infra.IoCs;

public static class AddServicesIoC
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IPythonEngineService, PythonEngineService>();
        return services;
    }
}
