using Domain.Entities.Gatherings.ValueObjects;
using Domain.Entities.Members.ValueObjects;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Invitations.DomainEvents;

public record InvitationCreatedDomainEvent(Guid gatheringId, Guid memberId) : IDomainEvent
{
}
