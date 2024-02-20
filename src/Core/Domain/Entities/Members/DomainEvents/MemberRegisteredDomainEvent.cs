using Domain.Entities.Members.ValueObjects;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Members.DomainEvents;

public record MemberRegisteredDomainEvent(Guid memberId) : IDomainEvent
{
}
