using Application.Response;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Members.Commands.CreateMember;

public class CreateMemberCommand : IRequest<AppResult<CreateMemberCommandResponse>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }    
    public string Email { get; set; }
}
