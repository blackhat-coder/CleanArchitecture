using Domain.Abstractions;
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

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<Gathering> Gatherings { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<Member> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
