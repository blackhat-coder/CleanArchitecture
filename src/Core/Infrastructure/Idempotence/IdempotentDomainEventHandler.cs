using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using Persistence.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Idempotence;

public sealed class IdempotentDomainEventHandler<TDomainEvent>(INotificationHandler<TDomainEvent> _decorator
    ,IApplicationDbContext _context, ILogger<IdempotentDomainEventHandler<TDomainEvent>> _logger) 
    : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        var handlerName = notification.GetType().Name;

        /// If message was consumed already
        /// Don't process message
        if(await _context.OutBoxMessageConsumer
            .AnyAsync(message => message.Id == notification.Id && message.Name == handlerName, cancellationToken))
        {
            _logger.LogInformation($"Outbox message with same Idempotency key:{notification.Id} already processed by {handlerName}");
            return;
        }

        await _decorator.Handle(notification, cancellationToken);
        await _context.OutBoxMessageConsumer.AddAsync(new OutboxMessageConsumer
        {
            Id = notification.Id,
            Name = handlerName
        });
        await _context.SaveChangesAsync(cancellationToken);

    }
}