using Application.DTOs.Common;
using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Members.Queries.GetMembers;

public class GetMembersQuery : BaseQueryDto, IRequest<AppResult<List<GetMembersQueryResponse>>> 
{

}
