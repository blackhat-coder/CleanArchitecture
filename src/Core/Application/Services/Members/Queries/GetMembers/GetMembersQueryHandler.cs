using Application.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Members.Queries.GetMembers;

public class GetMembersQueryHandler(IApplicationDbContext _context, ILogger<GetMembersQueryHandler> _logger, IMapper _mapper) 
    : IRequestHandler<GetMembersQuery, AppResult<List<GetMembersQueryResponse>>>
{
    public async Task<AppResult<List<GetMembersQueryResponse>>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var resp = await _context.Members
                .Skip(request.pageNum * request.pageSize)
                .Take(request.pageSize)
                .ToListAsync();

            var response = _mapper.Map<List<GetMembersQueryResponse>>(resp);

            _logger.LogInformation("Successfully retrieved memebers");
            return await AppResult<List<GetMembersQueryResponse>>.SuccessAsync("Successful", response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return await AppResult<List<GetMembersQueryResponse>>.ExceptionAsync(ex.Message);
        }
    }
}
