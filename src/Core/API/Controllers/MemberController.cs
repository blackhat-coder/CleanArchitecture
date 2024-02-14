using Application.Services.Members.Commands.CreateMember;
using Application.Services.Members.Commands.DeleteMember;
using Application.Services.Members.Queries.GetMemberById;
using Application.Services.Members.Queries.GetMembers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Runtime.InteropServices;

namespace API.Controllers;

[Route("api/[controller]")]
public class MemberController(IMediator _mediator) : BaseController
{
    [HttpPost("create-member")]
    public async Task<ActionResult> CreateMember(CreateMemberCommand request)
    {
        var response = await _mediator.Send(request);
        return ApiResponse(response);
    }

    [HttpGet("get-members")]
    public async Task<ActionResult> GetMembers([FromQuery] GetMembersQuery request)
    {
        var response = await _mediator.Send(request);
        return ApiResponse(response);
    }

    [HttpGet("get-member/{id}")]
    public async Task<ActionResult> GetMemberById(string id)
    {
        var response = await _mediator.Send(new GetMemberByIdQuery { MemberId = id });
        return ApiResponse(response);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteMember(string id)
    {
        var response = await _mediator.Send(new DeleteMemberCommand { MemberId = id });
        return ApiResponse(response);
    }
}
