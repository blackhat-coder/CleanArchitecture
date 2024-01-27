using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class Gathering
    {
        public static readonly ErrorDetails InvitingCreator = new ErrorDetails(GatheringCodes.InvitingCreator);
        public static readonly ErrorDetails AlreadyPassed = new ErrorDetails(GatheringCodes.AlreadyPassed);
    }
}
