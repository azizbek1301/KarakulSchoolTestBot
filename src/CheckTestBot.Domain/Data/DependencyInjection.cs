using CheckTestBot.Domain.Application.Abstruction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CheckTestBot.Domain.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IApplicationDbContext,BotDbContext>(options =>
                options.)
        }
    }
}
