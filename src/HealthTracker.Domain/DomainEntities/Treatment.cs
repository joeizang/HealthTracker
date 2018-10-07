using HealthTracker.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Domain.DomainEntities
{
    public class Treatment : IEntity
    {


        private Treatment()
        {

        }

        public string TreatmentId { get; private set; }

        public string TreatmentDescription { get; private set; }

        public string TreatedBy { get; private set; }

        public List<Ailment> Ailments { get; private set; }

        public List<Medication> Medications { get; private set; }

        public bool IsDeleted { get; private set; } = false;

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}
