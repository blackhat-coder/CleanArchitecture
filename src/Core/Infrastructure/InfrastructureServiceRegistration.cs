using Application.Abstractions;
using Infrastructure.HostedServices;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddHostedService<ProcessOutBoxMessagesHostedService>();
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
