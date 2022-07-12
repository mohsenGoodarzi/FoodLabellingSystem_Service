using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using System;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IIngredientTypeDAO
    {
        List<IngredientType> GetAll();
        public IngredientType GetById(string id);
        QueryResult Add(string ingredientTypeId, string member);
        QueryResult Update(string ingredientTypeId, string member);
        QueryResult Remove(string ingredientTypeId);

    }
}

