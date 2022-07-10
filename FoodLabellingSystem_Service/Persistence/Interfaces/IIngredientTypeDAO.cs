using FoodLabellingSystem_Service.Other;
using System;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IIngredientTypeDAO
    {
        SqlDataReader? GetAll();
        QueryResult Add(string ingredientTypeId, string member);
        QueryResult Update(string ingredientTypeId, string member);
        QueryResult Remove(string ingredientTypeId);

    }
}

