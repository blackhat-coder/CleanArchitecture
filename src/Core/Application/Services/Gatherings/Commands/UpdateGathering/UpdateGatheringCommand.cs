using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Commands.UpdateGathering;

public class UpdateGatheringCommand : IRequest<AppResult>
{
    public string MemberId { get; set; }
    public string GatheringId { get; set; }
    public string GatheringName { get; set; }
    public string location { get; set; }
    public DateTime ScheduledAt { get; set; }
}
