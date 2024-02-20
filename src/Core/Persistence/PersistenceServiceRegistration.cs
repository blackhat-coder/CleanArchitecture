using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var interceptor = serviceProvider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            options.UseNpgsql(configuration.GetConnectionString("Db"))
                .AddInterceptors(interceptor!);
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        /*services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());*/

        return services;
    }
}
