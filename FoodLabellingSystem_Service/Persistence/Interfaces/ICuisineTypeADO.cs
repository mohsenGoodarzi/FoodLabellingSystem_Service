using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using System;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface ICuisineTypeADO
    {
        public List<CuisineType> GetAll();
        public QueryResult Add(string cuisineTypeId, string member);
        public QueryResult Remove(string cuisineTypeId);
        public QueryResult Update(string cuisineTypeId, string member);

    }
}

