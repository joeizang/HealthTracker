using System;
using HealthTracker.Domain.Abstractions;

namespace HealthTracker.Domain.DomainEntities
{
    public class Dosage : IEntity
    {
        public string DosageId { get; private set; }
        public int Frequency { get; private set; }

        public float? Volume { get; private set; }

        public float? Quantity { get; private set; }

        public UnitMeasure UnitMeasure { get; private set; }

        public string Description { get; private set; }

        public string MedicationId { get; private set; }

        public Medication Medication { get; private set; }

        public bool IsDeleted { get; private set; } = false;

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}