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

namespace Application.Services.Gatherings.Queries.GetMemberGatherings;


// Gets gatherings where you are/were an attendee
public class GetMemberGatheringsQueryHandler(IApplicationDbContext _context, ILogger<GetMemberGatheringsQueryHandler> _logger) 
    : IRequestHandler<GetMemberGatheringsQuery, AppResult<List<GetMemberGatheringsQueryResponse>>>
{
    public async Task<AppResult<List<GetMemberGatheringsQueryResponse>>> Handle(GetMemberGatheringsQuery request, CancellationToken cancellationToken)
    {
        try
        {
#nullable disable
            MemberId memberId = new MemberId(Guid.Parse(request.MemberId));

            var responseQuery = _context.Gatherings.AsNoTracking()
                .Include(x => x.Attendees.Where(attendees => attendees.MemberId == memberId))
                .Include(x => x.Creator)
                .Select(gathering => new GetMemberGatheringsQueryResponse
                {
                    GatheringId = gathering.Id.Value.ToString(),
                    CreatorId = gathering.Creator.Id.Value.ToString(),
                    CreatorEmail = gathering.Creator.Email,
                    GatheringName = gathering.Name,
                    Location = gathering.Location,
                    ScheduledAt = gathering.ScheduledAtUtc,
                    Expired = gathering.ScheduledAtUtc < DateTime.UtcNow,
                });

            var responses = await responseQuery.Skip(request.pageNum * request.pageSize).Take(request.pageSize).ToListAsync();

            return await AppResult<List<GetMemberGatheringsQueryResponse>>.SuccessAsync("Retrieved your gatherings", responses);

        }catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return await AppResult<List<GetMemberGatheringsQueryResponse>>.ExceptionAsync(ex.Message);
        }
    }
}
