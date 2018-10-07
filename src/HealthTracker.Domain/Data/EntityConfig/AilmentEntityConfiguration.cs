using HealthTracker.Domain.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTracker.Domain.Data.EntityConfig
{
    public class AilmentEntityConfiguration : IEntityTypeConfiguration<Ailment>
    {
        public void Configure(EntityTypeBuilder<Ailment> builder)
        {
            builder.Property(a => a.AilmentId).HasMaxLength(50).IsRequired();
            builder.Property(a => a.AilmentName).HasMaxLength(100).IsRequired();
            builder.Property(a => a.StartDate).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.CreatedAt).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(500);
            builder.HasIndex(a => a.AilmentId).IsUnique();
            builder.HasMany(a => a.Symptoms).WithOne();
            builder.HasQueryFilter(a => a.IsDeleted.Equals(true));
        }
    }
}
