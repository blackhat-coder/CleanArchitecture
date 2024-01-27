using Domain.Enums;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public sealed class Invitation : BaseEntity
{
    public Invitation(Guid id, Member member, Gathering gathering)
        : base(id)
    {

    }
    public Guid GatheringId { get; private set; }
    public Guid MemberId { get; private set; }
    public InvitationStatus Status { get; private set; }
}
