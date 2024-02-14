using Application.Response;
using Domain.Entities.Gatherings.ValueObjects;
using Domain.Entities.Members.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Invitations.Commands.Create;

public class CreateInvitationCommandHandler(IApplicationDbContext _context, ILogger<CreateInvitationCommandHandler> _logger) 
    : IRequestHandler<CreateInvitationCommand, AppResult>
{
    public async Task<AppResult> Handle(CreateInvitationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var gatheringId = new GatheringId(Guid.Parse(request.GatheringId));
            var gathering = await _context.Gatherings.FirstOrDefaultAsync(x => x.Id == gatheringId);

            if (gathering == null)
            {
                return await AppResult.FailAsync($"Gathering not found with Id:{request.GatheringId}", HttpStatusCode.NotFound);
            }

            var inviteeId = new MemberId(Guid.Parse(request.InviteeId));
            if(gathering.Creator.Id == inviteeId)
            {
                return await AppResult.FailAsync($"Cannot invite gathering creator");
            }

            var invitee = await _context.Members.FirstOrDefaultAsync(x => x.Id == inviteeId);
            if(invitee == null) { 
                return await AppResult.FailAsync($"Invitee Not Found with Id:{request.InviteeId}", HttpStatusCode.NotFound);
            }
#nullable disable
            var invitation = gathering.CreateInvitation(invitee);

            await _context.Invitations.AddAsync(invitation.data);
            await _context.SaveChangesAsync();
            return await AppResult.SuccessAsync($"Member:{invitee.Email} has been invited for Gathering:{gathering.Name}");
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult.ExceptionAsync(ex.Message);
        }
    }
}
