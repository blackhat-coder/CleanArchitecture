using Domain.Abstractions;
using Domain.Entities.Attendees;
using Domain.Entities.Gatherings;
using Domain.Entities.Invitations;
using Domain.Entities.Members;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Persistence.Outbox;
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
    public DbSet<OutboxMessage> OutBoxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker.Entries<IBaseEntity>().Where(entry => entry.State == EntityState.Added 
                                || entry.State == EntityState.Modified);

        foreach(var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Entity.CreatedOn = DateTime.UtcNow;
                entity.Entity.ModifiedOn = DateTime.UtcNow;
            }

            if (entity.State == EntityState.Modified)
            {
                entity.Entity.ModifiedOn = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
