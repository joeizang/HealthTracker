using HealthTracker.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Domain.DomainEntities
{
    public class Medication : IEntity
    {


        public string MedicationId { get; private set; }

        public string Name { get; private set; }

        public Dosage Dosage { get; private set; }

        public Treatment Treatment { get; private set; }

        public string TreatmentId { get; private set; }

        public bool IsDeleted { get; private set; } = false;

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}
