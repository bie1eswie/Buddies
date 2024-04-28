using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Buddies.Application;
using Microsoft.Extensions.Logging;
using Buddies.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

IConfiguration config = builder.Build();
var builderHost = Host.CreateDefaultBuilder();
builderHost.ConfigureLogging(logger =>
{
    logger.AddConsole();
});

// Service Injection 
var _host = builderHost.ConfigureServices(services =>
{
    services.AddApplicationServices();
}).Build();

var app = _host.Services.GetRequiredService<IApp>();
await app.Execute();