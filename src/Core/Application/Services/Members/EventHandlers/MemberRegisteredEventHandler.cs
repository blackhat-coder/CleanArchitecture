using Application.Abstractions;
using Domain.Entities.Members.DomainEvents;
using Domain.Entities.Members.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Members.EventHandlers;

public class MemberRegisteredEventHandler(IEmailService _mailService, IApplicationDbContext _context) 
    : INotificationHandler<MemberRegisteredDomainEvent>
{
    public async Task Handle(MemberRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var id = new MemberId(notification.memberId);
        var member = await _context.Members.AsNoTracking()
            .FirstOrDefaultAsync(member => member.Id == id, cancellationToken: cancellationToken);

        if (member == null) { return; }

        await _mailService.SendMailAsync(member.Email, "You've registered on our platform");

        return;
    }
}
