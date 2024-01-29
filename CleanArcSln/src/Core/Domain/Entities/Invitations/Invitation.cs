using Domain.Entities.Attendees;
using Domain.Entities.Gatherings;
using Domain.Entities.Gatherings.ValueObjects;
using Domain.Entities.Invitations.Enums;
using Domain.Entities.Invitations.ValueObjects;
using Domain.Entities.Members;
using Domain.Entities.Members.ValueObjects;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Invitations;

#nullable disable
public sealed class Invitation : BaseEntity<InvitationId>
{
    public Invitation(InvitationId id, Member member, Gathering gathering)
        : base(id)
    {

    }
    public GatheringId GatheringId { get; private set; }
    public MemberId MemberId { get; private set; }
    public InvitationStatus Status { get; private set; }

    internal Attendee Accept()
    {
        Status = InvitationStatus.Accepted;
        ModifiedOn = DateTime.UtcNow;

        var attendee = new Attendee(this);

        return attendee;
    }
    internal void Expire()
    {
        Status = InvitationStatus.Expired;
        ModifiedOn = DateTime.UtcNow;
    }
}
