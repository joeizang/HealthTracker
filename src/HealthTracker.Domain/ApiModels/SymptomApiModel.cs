using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Domain.ApiModels
{
    public class SymptomApiModel
    {
        public string Id { get; set; }
        public string SymptomName { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartDate { get; set; }
    }

    public class SymptomSearchModel
    {
        public string SymptomName { get; set; }

        public string RandomWord { get; set; }

        public string SortOrder { get; set; }

        public DateTimeOffset? Date { get; set; }
    }

    public class SymptomInputModel
    {
        public string Id { get; set; }
        public string SymptomName { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
