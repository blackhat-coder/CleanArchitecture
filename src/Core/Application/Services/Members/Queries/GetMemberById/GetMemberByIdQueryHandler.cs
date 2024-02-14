using Application.Response;
using AutoMapper;
using Domain.Entities.Members.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Members.Queries.GetMemberById;

public class GetMemberByIdQueryHandler(IApplicationDbContext _context, ILogger<GetMemberByIdQueryHandler> _logger, IMapper _mapper) 
    : IRequestHandler<GetMemberByIdQuery, AppResult<GetMemberByIdQueryResponse>>
{
    public async Task<AppResult<GetMemberByIdQueryResponse>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var MemberId = new MemberId(Guid.Parse(request.MemberId));

            var member = await _context.Members.AsNoTracking().FirstOrDefaultAsync(mem => mem.Id == MemberId && mem.IsDeleted == false);

            if (member == null)
            {
                _logger.LogError("member is null");

                return await AppResult<GetMemberByIdQueryResponse>.FailAsync(
                    $"Couldn't Retrieve Member with Id:{request.MemberId}",
                    new List<string> { "Member is null" });
            }

            var response = _mapper.Map<GetMemberByIdQueryResponse>(member);

            _logger.LogInformation("success");
            return await AppResult<GetMemberByIdQueryResponse>.SuccessAsync("Success", response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult<GetMemberByIdQueryResponse>.ExceptionAsync(ex.Message);
        }
    }
}
