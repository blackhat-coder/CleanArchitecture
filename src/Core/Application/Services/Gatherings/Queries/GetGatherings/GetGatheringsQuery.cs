using Application.DTOs.Common;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Gatherings.Queries.GetGatherings;

public class GetGatheringsQuery : BaseQueryDto, IRequest<AppResult<List<GetGatheringsQueryResponse>>>
{
}
