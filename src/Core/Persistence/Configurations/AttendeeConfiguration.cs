using Domain.Entities.Attendees;
using Domain.Entities.Attendees.ValueObjects;
using Domain.Entities.Gatherings;
using Domain.Entities.Gatherings.ValueObjects;
using Domain.Entities.Members;
using Domain.Entities.Members.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations;

internal class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).HasConversion(
            attendee => attendee.Value,
            value => new AttendeeId(value)
            );

        builder.Property(a => a.MemberId).HasConversion(
            member => member.Value,
            value => new MemberId(value)
            );

        builder.Property(a => a.GatheringId).HasConversion(
            gathering => gathering.Value,
            value => new GatheringId(value)
            );

        builder.HasOne<Member>()
            .WithMany()
            .HasForeignKey(attendee => attendee.MemberId)
            .IsRequired();

        builder.HasOne<Gathering>()
            .WithMany()
            .HasForeignKey(attendee => attendee.GatheringId)
            .IsRequired();
    }
}
