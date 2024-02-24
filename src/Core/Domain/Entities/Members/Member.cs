using Domain.Entities.Members.DomainEvents;
using Domain.Entities.Members.ValueObjects;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Members;

public sealed class Member : BaseEntity<MemberId>
{
#nullable disable
    public Member() { }
    public Member(MemberId id, string firstName, string lastName, string email)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;

        this.RaiseDomainEvent(new MemberRegisteredDomainEvent(Guid.NewGuid(), this.Id.Value));
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
