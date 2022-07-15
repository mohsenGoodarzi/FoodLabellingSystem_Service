using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using System;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IDishTypeDAO
    {
        public List<DishType> GetAll();
        public DishType GetById(string id);
        public QueryResult Add(string dishTypeId, string member);
        public QueryResult Remove(string dishTypeId);
        public QueryResult Update(string dishTypeId, string member);
    }
}

