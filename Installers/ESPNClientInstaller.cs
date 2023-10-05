using FantasyPowersLeague.Services.ESPN;

namespace FantasyPowersLeague.Installers
{
    public static class ESPNClientInstaller
    {
        public static IServiceCollection AddESPNClient(this IServiceCollection services, IConfiguration configuration)
        {
            var ESPNClientUrl = configuration.GetConnectionString("EspnApi");

            services.AddSingleton<IESPNClient>(provider => {
                var clientFactory = provider.GetService<IHttpClientFactory>();
                return new ESPNClient(ESPNClientUrl, clientFactory.CreateClient(nameof(ESPNClient)));
            });

            return services;
        }
    }
}