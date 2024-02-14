using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Commands.DeleteGathering;

public class DeleteGatheringCommand : IRequest<AppResult>
{
    public string GatheringId { get; set; }
    public string CreatorId { get; set; }
}
