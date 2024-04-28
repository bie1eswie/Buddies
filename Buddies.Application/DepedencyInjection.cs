using Buddies.Application.Services;
using Buddies.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace Buddies.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBuddyService,BuddyService>();
            services.AddSingleton<IApp, App>();
            services.AddHttpClient<IBuddyService, BuddyService>()
                    .ConfigurePrimaryHttpMessageHandler(() =>
                    {
                        return new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = (m, crt, chn, e) => true
                        };
                    });
            return services;
        }
    }
}
