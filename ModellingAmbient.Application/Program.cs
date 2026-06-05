using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using ModellingAmbient.Core.Interfaces;
using ModellingAmbient.Core.Repositories;
using ModellingAmbient.Infra.Interfaces;
using ModellingAmbient.Infra.Services;
using ModellingAmbient.Infra.Settings;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<PythonEngineSettings>(
    builder.Configuration.GetSection(PythonEngineSettings.SectionName));

builder.Services.AddSingleton<IPythonEngineService, PythonEngineService>();
builder.Services.AddSingleton<IB3Repository, B3Repository>();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "MA API",
    });
});

builder.WebHost.ConfigureKestrel(o => o.Listen(IPAddress.Any, 5001));
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MA API v1");
});
app.MapControllers();
app.Run();