using Domain.Entities.Gatherings.ValueObjects;
using Domain.Entities.Invitations.ValueObjects;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Invitations.DomainEvents;

public sealed record InvitationAcceptedDomainEvent(Guid Id, Guid InvitationId, Guid GattheringId) : DomainEvent(Id)
{

}
