using CheckTestBot.Domain.Application.Service.Subject;
using CheckTestBot.Domain.Application.Service.User;
using Microsoft.Extensions.DependencyInjection;

namespace CheckTestBot.Domain.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IUserService, UserService>();


            return services;

        }
    }
}
