using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS_Service_Test
{
   
    internal class UnitTest
    {
        private IConfiguration configuration;
        private IUnitDAO unitDAO;

        [SetUp]
        public void SetUp()
        {
            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            unitDAO = new UnitDAO(configuration);
        }
       
        [Test]
        public void CreateUnit()
        {
            Unit expected = new Unit("Test",1.12);
            unitDAO.Add("Test",1.12);
            Unit fetched = unitDAO.GetById("Test");
            Assert.That(expected ,Is.EqualTo (fetched));
        }
        [Test]
        public void GetById()
        {
            Unit expected = new Unit("Test", 1.12);
            Unit fetched = unitDAO.GetById("Test");
            Assert.IsTrue(expected == fetched, "Expected unit == fetched unit.");
        }
        [Test]
        public void UpdateUnit()
        {
            Unit expected = new Unit("Test", 2.5);
            unitDAO.Update("Test", 2.5);
            Unit fetched = unitDAO.GetById("Test");
            Assert.That(expected, Is.EqualTo(fetched));
        }
        [Test]
        public void GetByIdNegative()
        {
            Unit expected = new Unit("Nothing", 0);
            Unit fetched = unitDAO.GetById("Test");
            Assert.That(expected, !Is.EqualTo(fetched));
        }
        [Test]
        public void RemoveUnit()
        {
            Unit expected = new Unit("Test", 2.5);
            unitDAO.Remove("Test");
            Unit fetched = unitDAO.GetById("Test");
            Assert.That(expected, !Is.EqualTo(fetched));
        }
        [Test]
        public void GetAll()
        {
            List<Unit> expected = new List<Unit>() {
                new Unit("cup",128),
                new Unit("gram",1),
                new Unit("mililitr",1),
                new Unit("mug",354.88),
                new Unit("Not Specified",0),
                new Unit("ounce",28.3495),
                new Unit("pint",568.26125),
                new Unit("table spoon",28.3495),
                new Unit("tea spoon",4.2),
            };
            List<Unit> fetched = unitDAO.getAll();
            Assert.That(fetched, Is.EqualTo(expected));
        }
    }
}
