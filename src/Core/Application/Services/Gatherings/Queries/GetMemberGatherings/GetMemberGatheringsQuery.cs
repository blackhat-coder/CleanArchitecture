using Application.DTOs.Common;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Queries.GetMemberGatherings;

public class GetMemberGatheringsQuery : BaseQueryDto, IRequest<AppResult<List<GetMemberGatheringsQueryResponse>>>
{
    public string? MemberId { get; set; }
}
