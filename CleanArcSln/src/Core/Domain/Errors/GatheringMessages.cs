using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors;

public static class GatheringMessages
{
    public const string InvitingCreator = "Can't send invitation to the gathering creator";
    public const string AlreadyPassed = "Can't send invitation for gathering in the past";
    public const string InvitationSent = "Successfully sent invitation";
}
