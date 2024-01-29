using Domain.Entities.Gatherings;
using Domain.Entities.Gatherings.ValueObjects;
using Domain.Entities.Invitations;
using Domain.Entities.Invitations.Enums;
using Domain.Entities.Invitations.ValueObjects;
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

internal class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id).HasConversion(
            invitation => invitation.Value,
            value => new InvitationId(value)
            );

        builder.Property(i => i.GatheringId).HasConversion(
            gathering => gathering.Value,
            value => new GatheringId(value)
            );

        builder.Property(i => i.MemberId).HasConversion(
            member => member.Value,
            value => new MemberId(value)
            );

        builder.Property(i => i.Status).HasConversion(
            status => status.ToString(),
            value => (InvitationStatus)Enum.Parse<InvitationStatus>(value)
            );

        builder.HasOne<Gathering>()
            .WithMany()
            .HasForeignKey(invitation => invitation.GatheringId)
            .IsRequired();


        builder.HasOne<Member>()
            .WithMany()
            .HasForeignKey(invitation => invitation.MemberId)
            .IsRequired();
    }
}
