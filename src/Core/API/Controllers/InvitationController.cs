using Application.Services.Invitations.Commands.Create;
using Application.Services.Invitations.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class InvitationController(IMediator _mediator) : BaseController
{
    [HttpPost("create-invitation")]
    public async Task<ActionResult> CreateInvitation(CreateInvitationCommand request)
    {
        var response = await _mediator.Send(request);
        return ApiResponse(response);
    }

    [HttpPost("accept-invitation")]
    public async Task<ActionResult> AccepteInvitation(UpdateInvitationCommand request)
    {
        var response = await _mediator.Send(request);
        return ApiResponse(response);
    }
}
