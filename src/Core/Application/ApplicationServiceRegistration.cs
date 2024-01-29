using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(m => m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
