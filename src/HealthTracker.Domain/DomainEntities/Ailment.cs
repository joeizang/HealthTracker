using HealthTracker.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Domain.DomainEntities
{
    public class Ailment : IEntity
    {

        private Ailment()
        {

        }

        public string AilmentId { get; private set; }

        public string AilmentName { get; private set; }

        public DateTimeOffset StartDate { get; private set; }

        public string Description { get; private set; }

        public List<Symptom> Symptoms { get; private set; }

        public bool IsDeleted { get; private set; } = false;

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}
