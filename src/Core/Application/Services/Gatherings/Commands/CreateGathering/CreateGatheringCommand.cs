using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Commands.CreateGathering;

public class CreateGatheringCommand : IRequest<AppResult<CreateGatheringCommandResponse>>
{
    public string MemberId { get; set; }
    public string GatheringName { get; set; }
    public string GatheringType { get; set; }
    public string location { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int MaxNumberOfAttendees { get; set; }
    public int InvitationValidBefore { get; set; }
}
