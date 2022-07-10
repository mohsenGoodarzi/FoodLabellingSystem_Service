using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IIngredientADO
    {
        public List<Ingredient> GetAll();
        public QueryResult Add(string ingredientId, string description, string ingredientTypeId, string unitId, string amount, double fat, double carbs, double protein, double calory, string warningId);
        public QueryResult Update(string ingredientId, string description, string ingredientTypeId, string unitId, string amount, double fat, double carbs, double protein, double calory, string warningId);
        public QueryResult Remove(string ingredientId);
    }
}
