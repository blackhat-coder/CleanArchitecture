using Application.Services.Members.Queries.GetMemberById;
using Application.Services.Members.Queries.GetMembers;
using AutoMapper;
using Domain.Entities.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile() {
        CreateMap<Member, GetMembersQueryResponse>()
            .ForMember(
            dest => dest.Id,
            src => src.MapFrom(x => x.Id.Value.ToString())
            );

        CreateMap<Member, GetMemberByIdQueryResponse>()
            .ForMember(
            dest => dest.Id,
            src => src.MapFrom(x => x.Id.Value.ToString())
            );
    }
}
