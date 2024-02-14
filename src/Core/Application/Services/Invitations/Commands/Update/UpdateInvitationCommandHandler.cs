using Application.Response;
using Domain.Entities.Invitations.ValueObjects;
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

namespace Application.Services.Invitations.Commands.Update;

public class UpdateInvitationCommandHandler(IApplicationDbContext _context, ILogger<UpdateInvitationCommandHandler> _logger) 
    : IRequestHandler<UpdateInvitationCommand, AppResult>
{
    public async Task<AppResult> Handle(UpdateInvitationCommand request, CancellationToken cancellationToken)
    {
        try
        {
#nullable disable
            var invitationId = new InvitationId(Guid.Parse(request.InvitationId));
            var invitation = await _context.Invitations.FirstOrDefaultAsync(x => x.Id == invitationId);
            if (invitation != null)
            {
                return await AppResult.FailAsync($"Invitation Not Found with Id:{request.InvitationId}", HttpStatusCode.NotFound);
            }

            var gathering = await _context.Gatherings.FirstOrDefaultAsync(x => x.Id == invitation.GatheringId);
            if (gathering == null)
            {
                return await AppResult.FailAsync($"Gathering not found with Id:{invitation.GatheringId.ToString()}", HttpStatusCode.NotFound);
            }

            var response = gathering.AcceptInvitation(invitation);

            await _context.SaveChangesAsync();

            if (response.IsSuccess)
                return await AppResult.SuccessAsync($"Invitation Accepted");

            return await AppResult.FailAsync(response.message);
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult.ExceptionAsync(ex.Message);
        }
    }
}
