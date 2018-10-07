using HealthTracker.Domain.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTracker.Domain.Data.EntityConfig
{
    public class TreatmentEntityConfiguration : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> builder)
        {
            builder.Property(t => t.TreatmentId).HasMaxLength(50).IsRequired();
            builder.Property(t => t.TreatedBy).HasMaxLength(50).IsRequired();
            builder.Property(t => t.TreatmentDescription).HasMaxLength(500);
            builder.Property(t => t.IsDeleted).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.HasIndex(t => t.TreatmentId).IsUnique();
            builder.HasMany(t => t.Ailments).WithOne();
            builder.HasMany(t => t.Medications).WithOne(m => m.Treatment)
                   .HasForeignKey(m => m.TreatmentId);

            builder.HasQueryFilter(t => t.IsDeleted.Equals(true));
        }
    }
}
