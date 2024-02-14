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
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Commands.DeleteGathering;

public class DeleteGatheringCommandHandler(IApplicationDbContext _context, ILogger<DeleteGatheringCommandHandler> _logger) 
    : IRequestHandler<DeleteGatheringCommand, AppResult>
{
    public async Task<AppResult> Handle(DeleteGatheringCommand request, CancellationToken cancellationToken)
    {
        try
        {
            GatheringId gatheringId = new GatheringId(Guid.Parse(request.GatheringId));
            MemberId creatorId = new MemberId(Guid.Parse(request.CreatorId));

            var gathering = await _context.Gatherings.Include(gathering => gathering.Creator)
                .FirstOrDefaultAsync(gathering => gathering.Id == gatheringId && gathering.Creator.Id == creatorId);

            if (gathering is null)
            {
                return await AppResult.FailAsync($"Unable to retrieve gathering with Id:{request.GatheringId}");
            }

            gathering.IsDeleted = true;
            await _context.SaveChangesAsync();

            return await AppResult.SuccessAsync($"Successfully deleted gathering with Id:{request.GatheringId}");
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult.ExceptionAsync(ex.Message);
        }
    }
}
