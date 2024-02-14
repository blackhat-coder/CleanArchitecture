using Domain.Entities.Gatherings;
using Domain.Entities.Gatherings.Enums;
using Domain.Shared;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Commands.CreateGathering;

internal class CreateGatheringCommandValidator : AbstractValidator<CreateGatheringCommand>
{
    public CreateGatheringCommandValidator()
    {
        RuleFor(x => x.MemberId).NotEmpty().WithMessage(GatheringMessages.InvalidMemberId);
        RuleFor(x => x.GatheringType).NotEmpty().WithMessage(GatheringMessages.InvalidGatheringType);
        RuleFor(x => x.GatheringName).NotEmpty().WithMessage(GatheringMessages.InvalidGatheringName);
        RuleFor(x => x.location).NotEmpty().WithMessage(GatheringMessages.InvalidLocation);
        RuleFor(x => x.ScheduledAt).NotEmpty().WithMessage(GatheringMessages.InvalidDate);

        RuleFor(x => x.GatheringType).Must(CheckGatheringType).WithMessage(GatheringMessages.InvalidGatheringType);
        RuleFor(x => x.GatheringType).Must((o, type) =>
        {
            return ConfirmGatheringTypeDepValues(o.GatheringType, o.MaxNumberOfAttendees, o.InvitationValidBefore);
        }).WithMessage(GatheringMessages.InvalidGatheringValue);
    }

    public bool CheckGatheringType(string type)
    {
        bool valid = false;
        if(Enum.TryParse(type, out GatheringType value))
        {
            valid = true;
        }

        return valid;
    }

    public bool ConfirmGatheringTypeDepValues(string type, int? MaxNumOfAttendees, int? invitationValidBefore)
    {
        bool valid = true;
        Enum.TryParse(type, out GatheringType value);

        if ( (value == GatheringType.FixedNumberOfAttendees && MaxNumOfAttendees == null) || MaxNumOfAttendees == 0)
        {
            valid = false;
        }

        if( (value == GatheringType.ExpirationForInvitations && invitationValidBefore == null) || invitationValidBefore == 0)
        {
            valid = false;
        }

        return valid;
    }
}
