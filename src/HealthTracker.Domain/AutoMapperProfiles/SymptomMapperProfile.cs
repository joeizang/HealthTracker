using AutoMapper;
using HealthTracker.Domain.ApiModels;
using HealthTracker.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Domain.AutoMapperProfiles
{
    public class SymptomMapperProfile : Profile
    {
        public SymptomMapperProfile()
        {
            CreateMap<Symptom, SymptomApiModel>();
        }
    }
}
