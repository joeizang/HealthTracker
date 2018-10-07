using AutoMapper;
using AutoMapper.QueryableExtensions;
using HealthTracker.Domain.ApiModels;
using HealthTracker.Domain.Data;
using HealthTracker.Domain.DomainEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthTracker.Domain.DataServices
{
    public class SymptomDataService
    {
        private readonly HealthTrackerContext _db;

        public SymptomDataService(HealthTrackerContext db)
        {
            _db = db;
        }

        //https://stackoverflow.com/questions/25182011/why-async-await-allows-for-implicit-conversion-from-a-list-to-ienumerable
        public Task<List<SymptomApiModel>> GetSymptoms()
        {
            return _db.Symptoms.Select(x => new SymptomApiModel
            {
                SymptomName = x.SymptomName,
                Description = x.Description,
                StartDate = x.CreatedAt
            }).ToListAsync();
        }

        public Task<List<SymptomApiModel>> GetSymptoms(SymptomSearchModel search = null)
        {
            if(search == null)
            {
                return GetSymptoms();
            }

            var searchname = search.SymptomName ?? "blank";

            var randomwords = search.RandomWord ?? "Random";

            var order = search.SortOrder ?? "Descending";

            var searchDate = search.Date ?? DateTimeOffset.UtcNow.Date;

            var query = _db.Symptoms.AsNoTracking().Where(s => !s.SymptomName.Equals("blank") && 
            s.SymptomName.Contains(searchname) || !s.Description.Equals("random") && s.Description.Contains(randomwords));

            switch (order)
            {
                case "Ascending":
                    query = query.OrderBy(q => q.SymptomName);
                    break;
                case "Date_Ascending":
                    query = query.OrderBy(q => q.CreatedAt);
                    break;
                default:
                    query = query.OrderBy(q => q.SymptomName)
                                 .OrderByDescending(q => q.CreatedAt);
                    break;
            }

            query = query.Skip(5).Take(10);

            return query.ProjectTo<SymptomApiModel>().ToListAsync();
        }

        public Task<SymptomApiModel> GetSymptomById(string id)
        {
            var query = _db.Symptoms.AsNoTracking().Where(s => s.SymptomId.Equals(id))
                                    .Select(s => new SymptomApiModel
                                    {
                                        Description = s.Description,
                                        Id = s.SymptomId,
                                        StartDate = s.CreatedAt,
                                        SymptomName = s.SymptomName
                                    }).SingleOrDefaultAsync();

            return query;

        }

        public Task<SymptomApiModel> UpdateSymptom(SymptomInputModel model)
        {
            //if they are here then they deserve to be here
            try
            {
                var targetmodel = _db.Symptoms.Find(model.Id);
                targetmodel.UpdateSymptom(model);
                _db.Symptoms.Update(targetmodel);
                _db.SaveChangesAsync();

                return GetSymptomById(model.Id);
            }
            catch (DbUpdateException ex)
            {
                throw;
            }
            catch(NullReferenceException ex)
            {
                throw;
            }
        }

        public Task<int> DeleteSymptom(string id)
        {
            try
            {
                var target = _db.Symptoms.Find(id);

                target.Delete(id);
                _db.Symptoms.Update(target);
                return _db.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
