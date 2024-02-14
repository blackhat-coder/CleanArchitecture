using Domain.Entities.Gatherings;
using Domain.Entities.Gatherings.Enums;
using Domain.Entities.Gatherings.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;

internal class GatheringConfiguration : IEntityTypeConfiguration<Gathering>
{
    public void Configure(EntityTypeBuilder<Gathering> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id).HasConversion(
            gathering => gathering.Value,
            value => new GatheringId(value)
            );

        builder.Property(g => g.Type).HasConversion(
            type => type.ToString(),
            value => (GatheringType)Enum.Parse<GatheringType>(value)
            );

        builder.HasMany(gathering => gathering.Attendees)
            .WithOne()
            .HasForeignKey(attendee => attendee.GatheringId);

        builder.HasMany(gathering => gathering.Invitations)
            .WithOne()
            .HasForeignKey(invitation => invitation.GatheringId);

        builder.HasOne(gathering => gathering.Creator)
            .WithMany();
    }
}
