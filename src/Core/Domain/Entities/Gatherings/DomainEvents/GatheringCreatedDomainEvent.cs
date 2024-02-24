using Domain.Entities.Gatherings.ValueObjects;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Gatherings.DomainEvents;

public record GatheringCreatedDomainEvent(Guid Id, Guid gatheringId) : DomainEvent(Id)
{
}
