using Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Members.Queries.GetMemberById;

public class GetMemberByIdQuery : IRequest<AppResult<GetMemberByIdQueryResponse>>
{
    public string MemberId { get; set; }
}
