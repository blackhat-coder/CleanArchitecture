using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Queries.GetGatherings;

public class GetGatheringsQueryHandler(IApplicationDbContext _context, ILogger<GetGatheringsQueryHandler> _logger) 
    : IRequestHandler<GetGatheringsQuery, AppResult<List<GetGatheringsQueryResponse>>>
{
    public async Task<AppResult<List<GetGatheringsQueryResponse>>> Handle(GetGatheringsQuery request, CancellationToken cancellationToken)
    {
        try
        { 
            var responseQuery = _context.Gatherings
                .AsNoTracking()
                .Include(x => x.Creator)
                .Select(gathering => new GetGatheringsQueryResponse {
                    GatheringId = gathering.Id.Value.ToString(),
                    CreatorId = gathering.Creator.Id.Value.ToString(),
                    CreatorEmail = gathering.Creator.Email,
                    GatheringName = gathering.Name,
                    Location = gathering.Location,
                    ScheduledAt = gathering.ScheduledAtUtc
            });

            var responses = await responseQuery.Skip(request.pageNum * request.pageSize).Take(request.pageSize).ToListAsync();

            return await AppResult<List<GetGatheringsQueryResponse>>.SuccessAsync("Retrieved gatherings", responses);
        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult<List<GetGatheringsQueryResponse>>.ExceptionAsync(ex.Message);
        }
    }
}
