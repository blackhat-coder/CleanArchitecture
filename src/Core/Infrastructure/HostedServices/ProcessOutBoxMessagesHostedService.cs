using Domain.Entities.Members.DomainEvents;
using Domain.Primitives;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Persistence.Context;
using Persistence.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.HostedServices;

public class ProcessOutBoxMessagesHostedService(IServiceScopeFactory _serviceFactory, 
    ILogger<ProcessOutBoxMessagesHostedService> _logger) : IHostedService, IDisposable
{

    private Timer? _timer = null;
    private static int BATCH_INDEX = 0;
    private static int BATCH_SIZE = 100;
    private static object _lock = new object();
    private object _indexLock = new object();
    public void Dispose()
    {
        _timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("OutBox message job starting...");

            _timer = new Timer(ProcessOutBoxMessages, null, 0, 9000);

            return Task.CompletedTask;
        }catch(Exception ex)
        {
            _logger.LogError($"OutBox message job failure: {ex.Message}");
            return Task.CompletedTask;
        }
    }

    private async void ProcessOutBoxMessages(object? state)
    {

        using(var scope = _serviceFactory.CreateScope())
        {
            try {
                var _context = (IApplicationDbContext)scope.ServiceProvider.GetRequiredService(typeof(IApplicationDbContext));
                var _publisher = (IPublisher)scope.ServiceProvider.GetRequiredService(typeof(IPublisher));

                List<OutboxMessage> messages;

                lock (_lock)
                {
                    _logger.LogInformation($"BATCH_INDEX:{BATCH_INDEX}, BATCH_SIZE:{BATCH_SIZE}");

                    messages = _context.OutBoxMessages
                        .Where(message => message.ProcessedOnUtc == null)
                        .OrderBy(message => message.OccurredOnUtc)
                        .Skip(BATCH_INDEX).Take(BATCH_SIZE)
                        .ToList();

                }
                var method = typeof(JsonConvert).GetMethods().FirstOrDefault(x => x.Name == "DeserializeObject" && x.IsGenericMethod == true);
                if (method == null)
                {
                    throw new Exception("Can't get DeserializeObject");
                };
                foreach (var message in messages)
                {

                    var type = Assembly.GetAssembly(typeof(IDomainEvent))?.GetTypes().FirstOrDefault(type => type.Name == message.Type);
                    
                    
                    if (type == null)
                    {
                        _logger.LogError("type is null");
                        continue;
                    };

                    var generic = method.MakeGenericMethod(type);

                    var domainEvent = generic.Invoke(this, new object[] { message.Content });
                    if (domainEvent is null)
                    {
                        _logger.LogError($"Deserialization error when trying to read outbox message, ID: {message.Id}");
                        continue;
                    }

                    await _publisher.Publish(domainEvent);
                    message.ProcessedOnUtc = DateTime.UtcNow;

                    lock (_indexLock)
                    {
                        BATCH_INDEX++;
                    }
                }

            await _context.SaveChangesAsync();
            } catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
        }

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        this.Dispose();
        return Task.CompletedTask;
    }
}
