using HealthTracker.Domain.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Domain.Data.EntityConfig
{
    public class DosageEntityConfiguration : IEntityTypeConfiguration<Dosage>
    {
        public void Configure(EntityTypeBuilder<Dosage> builder)
        {
            builder.Property(d => d.DosageId).HasMaxLength(150).IsRequired();
            builder.Property(d => d.Description).HasMaxLength(500);
            builder.Property(d => d.Frequency).IsRequired();
            builder.HasIndex(d => d.DosageId).IsUnique();
            builder.Property(d => d.CreatedAt).IsRequired();
            builder.Property(d => d.IsDeleted).IsRequired();
            builder.HasQueryFilter(d => d.IsDeleted.Equals(true));
        }
    }
}
