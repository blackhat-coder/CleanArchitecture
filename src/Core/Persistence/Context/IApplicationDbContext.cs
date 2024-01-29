using Domain.Entities.Attendees;
using Domain.Entities.Gatherings;
using Domain.Entities.Invitations;
using Domain.Entities.Members;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context;

public interface IApplicationDbContext
{
    DbSet<Attendee> Attendees { get; set; }
    DbSet<Gathering> Gatherings { get; set; }
    DbSet<Invitation> Invitations { get; set; }
    DbSet<Member> Members { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
