using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModellingAmbient.Infra.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModellingAmbient.Infra.IoCs;

public static class ConfigureSettingsIoC
{
    public static IServiceCollection ConfigureSettings(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<PythonEngineSettings>(
            config.GetSection(PythonEngineSettings.SectionName));
        return services;
    }
}
