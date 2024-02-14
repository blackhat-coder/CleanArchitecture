using Application.Services.Gatherings.Commands.CreateGathering;
using Application.Services.Gatherings.Commands.DeleteGathering;
using Application.Services.Gatherings.Commands.UpdateGathering;
using Application.Services.Gatherings.Queries.GetGatherings;
using Application.Services.Gatherings.Queries.GetMemberGatherings;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class GatheringController(IMediator _mediator) : BaseController
{

    [HttpPost("create-gathering")]
    public async Task<ActionResult> CreateGathering(CreateGatheringCommand request)
    {
        var response = await _mediator.Send(request);
        return ApiResponse(response);
    }

    [HttpPut("update-gathering")]
    public async Task<ActionResult> UpdateGathering(UpdateGatheringCommand request)
    {
        var rseponse = await _mediator.Send(request);
        return ApiResponse(rseponse);
    }

    [HttpDelete("delete-gathering")]
    public async Task<ActionResult> DeleteGathering(DeleteGatheringCommand request)
    {
        var response = await _mediator.Send(request);
        return ApiResponse(response);
    }

    [HttpGet("get-gatherings")]
    public async Task<ActionResult> GetAllGatherings([FromQuery] GetGatheringsQuery request)
    {
        var response = await _mediator.Send(request);
        return ApiResponse(response);
    }

    [HttpGet("get-member-gatherings/{id}")]
    public async Task<ActionResult> GetMemberGatherings(string id, [FromQuery] GetMemberGatheringsQuery request)
    {
        var response = await _mediator.Send(new GetMemberGatheringsQuery
        {
            MemberId = id,
            pageNum = request.pageNum,
            pageSize = request.pageSize,
        });

        return ApiResponse(response);
    }
}
