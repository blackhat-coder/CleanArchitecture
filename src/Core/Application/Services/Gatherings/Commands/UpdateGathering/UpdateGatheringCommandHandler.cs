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

namespace Application.Services.Gatherings.Commands.UpdateGathering;

public class UpdateGatheringCommandHandler(IApplicationDbContext _context, ILogger<UpdateGatheringCommandHandler> _logger) 
    : IRequestHandler<UpdateGatheringCommand, AppResult>
{
    public async Task<AppResult> Handle(UpdateGatheringCommand request, CancellationToken cancellationToken)
    {
        try
        {

            GatheringId gatheringId = new GatheringId(Guid.Parse(request.GatheringId));
            MemberId memberId = new MemberId(Guid.Parse(request.MemberId));

            var gathering = await _context.Gatherings
                .Include(gathering => gathering.Creator)
                .FirstOrDefaultAsync(gathering => gathering.Id == gatheringId && gathering.Creator.Id == memberId);

            if (gathering == null) {
                return await AppResult.FailAsync("Update Failed: Gathering not found", HttpStatusCode.NotFound);
            }

            var updatedGathering = gathering.Update(request.GatheringName, request.location, request.ScheduledAt);

            await _context.SaveChangesAsync();
            return await AppResult.SuccessAsync("Successfully Updated Gathering");
            
        }catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult.ExceptionAsync(ex.Message);
        }
    }
}
