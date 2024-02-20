using Domain.Primitives;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using Persistence.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
        )
    {
        // Get the db Context
        var dbContext = eventData.Context;
        if(dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry> entries = dbContext.ChangeTracker.Entries<IBaseEntity>();

        var entities = entries.Select(x => x.Entity);

        var outboxmessages = dbContext.ChangeTracker.Entries<IBaseEntity>().Select(x => x.Entity).
            SelectMany(root =>
            {
                var domainEvents = root.GetDomainEvents();
                root.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
            }).ToList();

        dbContext.Set<OutboxMessage>().AddRange(outboxmessages);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
