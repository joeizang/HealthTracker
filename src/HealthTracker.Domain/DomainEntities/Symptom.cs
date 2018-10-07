using System;
using HealthTracker.Domain.Abstractions;
using HealthTracker.Domain.ApiModels;

namespace HealthTracker.Domain.DomainEntities
{
    public class Symptom : IEntity
    {


        private Symptom()
        {

        }

        public Symptom(string name, string description = "")
        {
            SymptomId = Guid.NewGuid().ToString();
            SymptomName = name;
            Description = description;
        }

        public void UpdateSymptom(SymptomInputModel model)
        {
            SymptomName = model.SymptomName;
            //SymptomId = model.Id;
            Description = model.Description;
            UpdatedAt = model.UpdatedDate;
        }

        public void Delete(string id)
        {
            if (SymptomId.Equals(id))
            {
                IsDeleted = true;
            }
        }

        public string SymptomId { get; private set; }

        public string SymptomName { get; private set; }

        public string Description { get; private set; }

        public bool IsDeleted { get; private set; } = false;

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? UpdatedAt { get; private set; }
    }
}