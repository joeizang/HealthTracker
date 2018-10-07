using HealthTracker.Domain.ApiModels;
using HealthTracker.Domain.Data;
using HealthTracker.Domain.DataServices;
using HealthTracker.Domain.DomainEntities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        public Tests()
        {

        }

        public DbContextOptions<HealthTrackerContext> Opts { get; private set; }
        public SqliteConnection Connection { get; private set; }

        [Test]
        public async Task Test1()
        {
            //Arrange
            Connection = new SqliteConnection("DataSource=../../../testdb.db3");

            Opts = new DbContextOptionsBuilder<HealthTrackerContext>()
                           .UseSqlite(Connection).Options;


            using (var db = new HealthTrackerContext(Opts))
            {
                db.Database.EnsureCreated();
                if (db.Database.IsSqlite())
                {
                    Console.WriteLine("Created Successfully and Database is SQLite!");
                }
                //Console.WriteLine($"{db.Symptoms.Count()} records in symptoms table! ${db.Symptoms.First().SymptomId.ToString()}" +
                //$"first record in database");
            }
            //Act
            using (var db = new HealthTrackerContext(Opts))
            {
                if (db.Symptoms.Count() < 1)
                {
                    var symptoms = new List<Symptom>
                    {
                        new Symptom("First Symptom", "The First one found"),
                        new Symptom("Second Symptom", "The Second one found")
                    };
                    db.Add(symptoms[0]);
                    db.Add(symptoms[1]);
                    db.SaveChanges();
                }
            }
            //Assert
            using (var db = new HealthTrackerContext(Opts))
            {
                var service = new SymptomDataService(db);
                var results = await service.GetSymptoms();
                Assert.AreEqual(2, results.Count);
            }
        }

        [Test]
        public async Task ItReturnsSymptomApiModelIfInputModelIsNull()
        {

            Connection = new SqliteConnection("DataSource=../../../testdb.db3");

            Opts = new DbContextOptionsBuilder<HealthTrackerContext>()
                           .UseSqlite(Connection).Options;

            using (var db = new HealthTrackerContext(Opts))
            {
                var service = new SymptomDataService(db);

                //Act
                var result = await service.GetSymptoms(null);

                //Assert
                Assert.IsInstanceOf<List<SymptomApiModel>>(result);
                Assert.AreEqual(2, result.Count);
            }
        }

        [Test]
        public void NameShouldBeBlankWhenSetToNothing()
        {
            var input = new SymptomSearchModel();

            var value = input.SymptomName ?? "Blank";

            input.SymptomName = null;

            Assert.AreEqual(value, "Blank");
        }

        [Test]
        public void RandomSearchWordShouldRandomWhenSetToNothing()
        {
            var input = new SymptomSearchModel();
            var value = input.RandomWord ?? "Random";

            //Act
            input.RandomWord = null;

            //Assert
            Assert.AreEqual(value, "Random");
        }

        [Test]
        public async Task GetSymptomByIdReturnsSingleSymptomApiModel()
        {
            Connection = new SqliteConnection("DataSource=../../../testdb.db3");

            Opts = new DbContextOptionsBuilder<HealthTrackerContext>()
                           .UseSqlite(Connection).Options;

            using (var db = new HealthTrackerContext(Opts))
            {
                var service = new SymptomDataService(db);

                //Act
                var result = await service.GetSymptomById("c89b00bd-289b-4b63-a4c5-46fe11e12f2c");

                //Assert
                Assert.IsInstanceOf<SymptomApiModel>(result);
            }
        }

        [Test]
        public async Task ItThrowsNullReferenceExceptionWhenInputModelFieldsAreEmpty()
        {
            var inputmodel = new SymptomInputModel();


            Connection = new SqliteConnection("DataSource=../../../testdb.db3");

            Opts = new DbContextOptionsBuilder<HealthTrackerContext>()
                           .UseSqlite(Connection).Options;

            using (var db = new HealthTrackerContext(Opts))
            {
                var service = new SymptomDataService(db);

                Assert.ThrowsAsync<NullReferenceException>(async () => await service.UpdateSymptom(inputmodel));
            }
        }

        [Test]
        public async Task UpdateSymptomReturnsUpdated()
        {
            Connection = new SqliteConnection("DataSource=../../../testdb.db3");

            Opts = new DbContextOptionsBuilder<HealthTrackerContext>()
                           .UseSqlite(Connection).Options;

            using (var db = new HealthTrackerContext(Opts))
            {
                var service = new SymptomDataService(db);
                var inputmodel = new SymptomInputModel
                {
                    SymptomName = "FirstSymptomUpdated",
                    UpdatedDate = DateTimeOffset.UtcNow,
                    Description = "I just Updated this",
                    Id = "c89b00bd-289b-4b63-a4c5-46fe11e12f2c"
                };

                //Act
                var result = await service.UpdateSymptom(inputmodel);

                //Assert
                Assert.IsTrue(result.SymptomName.Contains("Updated"));
                Assert.IsInstanceOf<SymptomApiModel>(result);
            }
        }

        [Test]
        public async Task DeleteThrowsExceptionWhenIdDoesNotExist()
        {
            Connection = new SqliteConnection("DataSource=../../../testdb.db3");

            Opts = new DbContextOptionsBuilder<HealthTrackerContext>()
                           .UseSqlite(Connection).Options;

            using (var db = new HealthTrackerContext(Opts))
            {
                var service = new SymptomDataService(db);

                //Act

                //Assert
                Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteSymptom("somedumbId"));
            }
        }

        [Test]
        public async Task DeleteChangesEntityStatusToDeleted()
        {
            Connection = new SqliteConnection("DataSource=../../../testdb.db3");

            Opts = new DbContextOptionsBuilder<HealthTrackerContext>()
                           .UseSqlite(Connection).Options;

            using (var db = new HealthTrackerContext(Opts))
            {
                var service = new SymptomDataService(db);

                var id = "c89b00bd-289b-4b63-a4c5-46fe11e12f2c";

                var symptom = await service.DeleteSymptom(id);

                var result = await db.Symptoms.AsNoTracking().SingleOrDefaultAsync(s => s.SymptomId.Equals(id));

                Assert.IsTrue(result.IsDeleted);
            }
        }
    }
}