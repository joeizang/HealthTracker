using HealthTracker.Domain.DomainEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTracker.Domain.Data
{
    public class HealthTrackerContext : DbContext
    {

        public HealthTrackerContext()
        {

        }

        public HealthTrackerContext(DbContextOptions<HealthTrackerContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }

        public DbSet<Ailment> Ailments { get; set; }

        public DbSet<Symptom> Symptoms { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Medication> Medications { get; set; }

        public DbSet<Dosage> Dosages { get; set; }
    }
}
