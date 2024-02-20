using Application.Abstractions;
using Domain.Entities.Gatherings.DomainEvents;
using Domain.Entities.Gatherings.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.EventHandlers;

public class GatheringCreatedDomainEventHandler(IApplicationDbContext _context, ILogger<GatheringCreatedDomainEventHandler> _logger,
    IEmailService _mailService ) : INotificationHandler<GatheringCreatedDomainEvent>
{
    public async Task Handle(GatheringCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var id = new GatheringId(notification.gatheringId);
        var gathering = await _context.Gatherings.AsNoTracking().FirstOrDefaultAsync(
                gathering => gathering.Id == id,
                cancellationToken
            );

        if(gathering == null)
        {
            _logger.LogError($"Invalid gathering, Id:{notification.gatheringId}");
            return;
        }

        await _mailService.SendMailAsync(gathering.Creator.Email, $"We've created your event with Name: {gathering.Name}");
        return;
    }
}
