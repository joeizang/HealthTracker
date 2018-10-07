using HealthTracker.Domain.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Domain.Data.EntityConfig
{
    public class MedicationEntityConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.Property(m => m.CreatedAt).IsRequired();
            builder.Property(m => m.IsDeleted).IsRequired();
            builder.Property(m => m.MedicationId).IsRequired().HasMaxLength(50);
            builder.Property(m => m.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(m => m.MedicationId).IsUnique();
            builder.HasIndex(m => m.Name);
            builder.HasOne(m => m.Dosage).WithOne(d => d.Medication)
                   .HasForeignKey<Dosage>(d => d.MedicationId);
            builder.HasQueryFilter(m => m.IsDeleted.Equals(true));
        }
    }
}
