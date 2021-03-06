using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IIngredientDAO
    {
        public List<Ingredient> GetAll();
        public Ingredient GetById(string id);
        public QueryResult Add(string ingredientId, string description, string ingredientTypeId, string unitId, double amount, double fat, double carbs, double protein, double calory, string warningId);
        public QueryResult Update(string ingredientId, string description, string ingredientTypeId, string unitId, double amount, double fat, double carbs, double protein, double calory, string warningId);
        public QueryResult Remove(string ingredientId);
    }
}
