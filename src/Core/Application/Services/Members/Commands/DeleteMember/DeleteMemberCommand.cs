using Application.Response;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Members.Commands.DeleteMember;

public class DeleteMemberCommand : IRequest<AppResult>
{
    public string MemberId { get; set; }
}
