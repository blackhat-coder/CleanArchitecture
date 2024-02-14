using Application.Response;
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

namespace Application.Services.Members.Commands.DeleteMember;

public class DeleteMemberCommandHandler(IApplicationDbContext _context, ILogger<DeleteMemberCommandHandler> _logger) 
    : IRequestHandler<DeleteMemberCommand, AppResult>
{
    public async Task<AppResult> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var MemberId = new MemberId(Guid.Parse(request.MemberId));
            var member = await _context.Members.FirstOrDefaultAsync(mem => mem.Id == MemberId);

            if (member == null) {
                _logger.LogError("Member is null");
                return await AppResult.FailAsync("Couldn't retrieve member");
            }

            member.IsDeleted = true;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted Member");
            return await AppResult.SuccessAsync("Successfully deleted member");
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult.ExceptionAsync(ex.Message);
        }
    }
}
