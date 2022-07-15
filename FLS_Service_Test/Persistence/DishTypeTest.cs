using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Persistence;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS_Service_Test.Persistence
{
    internal class DishTypeTest
    {
        private IConfiguration configuration;
        private IDishTypeDAO dishTypeDAO;


        [SetUp]
        public void SetUp() {

            configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            dishTypeDAO = new DishTypeDAO(configuration);
        
        }
        [Test, Sequential]
        public void Add() { 
      
            DishType expected = new DishType("Asian","Asian");
            dishTypeDAO.Add("Asian", "Asian");
            DishType fetched = dishTypeDAO.GetById("Asian");
            Assert.That(fetched, Is.EqualTo(expected));

        }

        [Test, Sequential]
        public void Update() {

            DishType expected = new DishType("Asian", "Not Specified");
            dishTypeDAO.Update("Asian", "Not Specified");
            DishType fetched = dishTypeDAO.GetById("Asian");
            Assert.That(fetched, Is.EqualTo(expected));    
        }
        [Test, Sequential]
        public void Delete() { 
        
        }
        [Test, Sequential]
        public void GetById() { 
        
        }
        [Test, Sequential]
        public void GetAll() { 
        
        }
    }
}
