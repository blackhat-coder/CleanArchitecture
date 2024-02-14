using Application.Response;
using Application.Utils;
using Domain.Entities.Gatherings;
using Domain.Entities.Gatherings.Enums;
using Domain.Entities.Gatherings.ValueObjects;
using Domain.Entities.Members.ValueObjects;
using Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Commands.CreateGathering;

public class CreateGatheringCommandHandler(IApplicationDbContext _context, ILogger<CreateGatheringCommand> _logger) 
    : IRequestHandler<CreateGatheringCommand, AppResult<CreateGatheringCommandResponse>>
{
    public async Task<AppResult<CreateGatheringCommandResponse>> Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateGatheringCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return await AppResult<CreateGatheringCommandResponse>.FailAsync("Validation error", validationResult.TransformToList());
            }

            Enum.TryParse(request.GatheringType, out GatheringType gatheringType);

            MemberId creatorId = new MemberId(Guid.Parse(request.MemberId));
            var creator = await _context.Members.FirstOrDefaultAsync(mem => mem.Id == creatorId);

            if (creator is null)
            {
                return await AppResult<CreateGatheringCommandResponse>.FailAsync("Can't retrieve Member", new List<string> { });
            }

            var gathering = Gathering.Create(GatheringId.NewGatheringId(), creator, gatheringType, request.ScheduledAt, request.GatheringName,
                request.location, request.MaxNumberOfAttendees, request.InvitationValidBefore);

            await _context.Gatherings.AddAsync(gathering);
            await _context.SaveChangesAsync();

            var response = new CreateGatheringCommandResponse
            {
                GatheringId = gathering.Id.Value.ToString(),
                CreatorId = request.MemberId,
                CreatorEmail = creator.Email,
                GatheringName = gathering.Name,
                location = gathering.Location,
                ScheduledAt = gathering.ScheduledAtUtc
            };

            _logger.LogInformation("Created Gathering");
            return await AppResult<CreateGatheringCommandResponse>.SuccessAsync("Successfully Created Gathering", response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult<CreateGatheringCommandResponse>.ExceptionAsync(ex.Message);
        }
    }
}
