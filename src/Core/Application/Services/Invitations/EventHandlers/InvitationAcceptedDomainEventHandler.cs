using Application.Abstractions;
using Domain.Entities.Invitations.DomainEvents;
using Domain.Entities.Invitations.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Invitations.EventHandlers;

internal class InvitationAcceptedDomainEventHandler(IEmailService _mailClient, IApplicationDbContext _context) 
    : INotificationHandler<InvitationAcceptedDomainEvent>
{
    public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
    {
        var id = new InvitationId(notification.InvitationId);
        var invitation = await _context.Invitations
            .FirstOrDefaultAsync(invitation => invitation.Id == id, cancellationToken);

        if(invitation == null) { return; }

        var member = await _context.Members
            .FirstOrDefaultAsync(member => member.Id == invitation.MemberId, cancellationToken);

        if (member == null) { return; }

        await _mailClient.SendMailAsync(member.Email, "Invitation Accepted, You have your spot \n You've accepted invitation to");

        return;
    }
}
