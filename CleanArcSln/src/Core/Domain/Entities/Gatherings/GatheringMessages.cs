using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Gatherings;

public static class GatheringMessages
{
    public const string InvitingCreator = "Can't send invitation to the gathering creator";
    public const string AlreadyPassed = "Can't send invitation for gathering in the past";
    public const string InvitationSent = "Successfully sent invitation";
    public const string InvitationExpired = "This invitation has expired, please contact the creator";
    public const string InvitationAccepted = "You've successfully accepted the invitation";
}
