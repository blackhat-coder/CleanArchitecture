using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Invitations.Commands.Create;

public class CreateInvitationCommand : IRequest<AppResult>
{
    public string GatheringId { get; set; }
    public string InviteeId { get; set; }
}
