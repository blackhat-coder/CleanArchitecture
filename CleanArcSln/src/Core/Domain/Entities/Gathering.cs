using Domain.Enums;
using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public sealed class Gathering : BaseEntity
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();

    private Gathering(Guid id, Member creator, GatheringType type, DateTime scheduledAtUtc, string name, string location)
        : base(id)
    {
        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;
    }

    public Member Creator { get; private set; }
    public GatheringType Type { get; private set; }
    public DateTime ScheduledAtUtc { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public int MaximumNumberOfAttendees { get; private set; }
    public DateTime InvitationsExpireAtUtc { get; private set; }


    public static Gathering Create(Guid id, Member creator, GatheringType type, DateTime scheduledAt, string name, string location,
        int maximumNumberOfAttendees, int invitationsValidBeforeInHours)
    {
        var gathering = new Gathering(id, creator, type, scheduledAt, name, location);

        gathering.CalculateGatheringTypeDetails(maximumNumberOfAttendees, invitationsValidBeforeInHours);

        return gathering;
    }

    private void CalculateGatheringTypeDetails(
        int? maximumNumberOfAttendees, int? invitationsValidBeforeInHours
        )
    {
        switch (Type)
        {
            case GatheringType.FixedNumberOfAttendees:
                if ( maximumNumberOfAttendees is null)
                {
                    throw new ArgumentNullException(nameof(maximumNumberOfAttendees));
                }

                MaximumNumberOfAttendees = maximumNumberOfAttendees.Value;
                break;
            case GatheringType.ExpirationForInvitations:
                if (invitationsValidBeforeInHours is null)
                {
                    throw new ArgumentNullException(nameof(invitationsValidBeforeInHours));
                }

                InvitationsExpireAtUtc = ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(GatheringType));
        }
    }

    public Result<Invitation> SendInvitation(Member member)
    {
        if (Creator.Id == member.Id)
        {
            return Result<Invitation>
                .Fail(GatheringMessages.InvitingCreator, DomainErrors.Gathering.InvitingCreator);
        }

        if (ScheduledAtUtc < DateTime.UtcNow)
        {
            return Result<Invitation>
                .Fail(GatheringMessages.AlreadyPassed, DomainErrors.Gathering.AlreadyPassed);
        }

        var invitation = new Invitation(Guid.NewGuid(), member, this);

        _invitations.Add(invitation);

        return Result<Invitation>.Success(GatheringMessages.InvitationSent, invitation);
    }

    public Attendee? AcceptInvitation(Invitation invitation)
    {

    }
}
