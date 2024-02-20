using Domain.Entities.Gatherings.ValueObjects;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Gatherings.DomainEvents;

public record GatheringCreatedDomainEvent(Guid gatheringId) : IDomainEvent
{
}
