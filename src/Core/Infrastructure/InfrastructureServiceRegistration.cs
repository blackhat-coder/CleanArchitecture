using Application.Abstractions;
using Infrastructure.HostedServices;
using Infrastructure.Idempotence;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddMediatR(m => m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddHostedService<ProcessOutBoxMessagesHostedService>();
        services.AddScoped<IEmailService, EmailService>();

        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        return services;
    }
}
