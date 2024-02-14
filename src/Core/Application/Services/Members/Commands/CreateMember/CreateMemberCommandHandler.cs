using Application.Response;
using Application.Utils;
using Domain.Entities.Members;
using Domain.Entities.Members.ValueObjects;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Members.Commands.CreateMember;

public class CreateMemberCommandHandler(IApplicationDbContext _context, ILogger<CreateMemberCommandHandler> _logger) 
    : IRequestHandler<CreateMemberCommand, AppResult<CreateMemberCommandResponse>>
{
    public async Task<AppResult<CreateMemberCommandResponse>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validator = new CreateMemberCommandValidator(_context);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation Error");
                return await AppResult<CreateMemberCommandResponse>.FailAsync("Member Validation Error", validationResult.TransformToList());
            }

            var member = new Member(MemberId.NewMemberId(), request.FirstName, request.LastName, request.Email);

            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();

            var response = new CreateMemberCommandResponse
            {
                MemberId = member.Id.Value.ToString()
            };

            _logger.LogInformation("Successfully Created member");
            return await AppResult<CreateMemberCommandResponse>.SuccessAsync("Successfully Created Member", response);
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult<CreateMemberCommandResponse>.ExceptionAsync(ex.Message);
        }
    }
}
