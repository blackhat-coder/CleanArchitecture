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

internal class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(
            member => member.Value,
            value => new MemberId(value)
            );

        builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();
    }
}
