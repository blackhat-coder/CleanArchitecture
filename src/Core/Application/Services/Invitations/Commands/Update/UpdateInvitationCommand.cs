using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Invitations.Commands.Update;

public class UpdateInvitationCommand : IRequest<AppResult>
{
    public string InvitationId { get; set; }
}
