using HealthTracker.Domain.DomainEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTracker.Domain.Data.EntityConfig
{
    public class SymptomEntityConfiguration : IEntityTypeConfiguration<Symptom>
    {
        public void Configure(EntityTypeBuilder<Symptom> builder)
        {
            builder.Property(s => s.SymptomId).HasMaxLength(50).IsRequired();
            builder.Property(s => s.SymptomName).HasMaxLength(30).IsRequired();
            builder.Property(s => s.IsDeleted).IsRequired();
            builder.Property(s => s.Description).HasMaxLength(500);
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.HasIndex(s => s.SymptomName);
            builder.HasIndex(s => s.SymptomId).IsUnique();
            builder.HasQueryFilter(s => s.IsDeleted.Equals(true));
        }
    }
}
