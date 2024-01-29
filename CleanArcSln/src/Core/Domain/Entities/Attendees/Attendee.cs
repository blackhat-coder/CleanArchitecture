using Domain.Entities.Attendees.ValueObjects;
using Domain.Primitives;
using Domain.Entities.Invitations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Members.ValueObjects;
using Domain.Entities.Gatherings.ValueObjects;

namespace Domain.Entities.Attendees;

public class Attendee : BaseEntity<AttendeeId>
{
    internal Attendee(Invitation invitation)
    {
        GatheringId = invitation.GatheringId;
        MemberId = invitation.MemberId;
    }
    public MemberId MemberId { get; private set; }
    public GatheringId GatheringId { get; private set; }
}
