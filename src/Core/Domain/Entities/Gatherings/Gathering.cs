using Domain.Entities.Attendees;
using Domain.Entities.Gatherings.DomainEvents;
using Domain.Entities.Gatherings.Enums;
using Domain.Entities.Gatherings.ValueObjects;
using Domain.Entities.Invitations;
using Domain.Entities.Invitations.DomainEvents;
using Domain.Entities.Invitations.ValueObjects;
using Domain.Entities.Members;
using Domain.Entities.Members.ValueObjects;
using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Gatherings;

// AggregateRoot
public sealed class Gathering : BaseEntity<GatheringId>
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<Attendee> _attendees = new();

#nullable disable
    public Gathering() { }

    private Gathering(GatheringId id, Member creator, GatheringType type, DateTime scheduledAtUtc, string name, string location)
        : base(id)
    {
        Creator = creator;
        Type = type;
        ScheduledAtUtc = scheduledAtUtc;
        Name = name;
        Location = location;

        RaiseDomainEvent(new GatheringCreatedDomainEvent(id.Value));
    }

    public Member Creator { get; private set; }
    public GatheringType Type { get; private set; }
    public DateTime ScheduledAtUtc { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public int NumberOfAttendees { get; private set; }
    public int MaximumNumberOfAttendees { get; set; }
    public DateTime InvitationsExpireAtUtc { get; private set; }
    public IReadOnlyCollection<Attendee> Attendees => _attendees;
    public IReadOnlyCollection<Invitation> Invitations => _invitations;


    public static Gathering Create(GatheringId id, Member creator, GatheringType type, DateTime scheduledAt, string name, string location,
        int maximumNumberOfAttendees, int invitationsValidBeforeInHours)
    {
        var gathering = new Gathering(id, creator, type, scheduledAt, name, location);

        gathering.CalculateGatheringTypeDetails(maximumNumberOfAttendees, invitationsValidBeforeInHours);

        return gathering;
    }

    public Gathering Update(string name, string location, DateTime scheduledAt)
    {
        this.Name = name;
        this.Location = location;
        this.ScheduledAtUtc = scheduledAt;

        return this;
    }

    private void CalculateGatheringTypeDetails(
        int? maximumNumberOfAttendees, int? invitationsValidBeforeInHours
        )
    {
        switch (Type)
        {
            case GatheringType.FixedNumberOfAttendees:
                if (maximumNumberOfAttendees is null)
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

    public Result<Invitation> CreateInvitation(Member member)
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

        var invitation = new Invitation(InvitationId.NewInvitationId(), member, this);

        _invitations.Add(invitation);
        RaiseDomainEvent(new InvitationCreatedDomainEvent(this.Id.Value, member.Id.Value));

        return Result<Invitation>.Success(GatheringMessages.InvitationSent, invitation);
    }

    public Result<Attendee> AcceptInvitation(Invitation invitation)
    {
        var expired = Type == GatheringType.FixedNumberOfAttendees && NumberOfAttendees == MaximumNumberOfAttendees ||
            Type == GatheringType.ExpirationForInvitations && InvitationsExpireAtUtc < DateTime.UtcNow;

        if (expired)
        {
            invitation.Expire();
            return Result<Attendee>.Fail(GatheringMessages.InvitationExpired, DomainErrors.Gathering.InvitationExpired);
        }

        var attendee = invitation.Accept();
        _attendees.Add(attendee);
        NumberOfAttendees++;

        RaiseDomainEvent(new InvitationAcceptedDomainEvent(invitation.Id.Value, this.Id.Value));

        return Result<Attendee>.Success(GatheringMessages.InvitationAccepted, attendee);
    }
}
