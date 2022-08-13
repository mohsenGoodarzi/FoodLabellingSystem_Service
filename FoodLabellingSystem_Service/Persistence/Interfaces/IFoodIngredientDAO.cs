using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IFoodIngredientDAO
    {
        public List<FoodIngredient> GetAll();
        public FoodIngredient GetById(string foodId, string ingredientId);
        public QueryResult Add(string foodId, string ingredientId, string unitId, double amount);
        public QueryResult Remove(string foodId, string ingredientId);
        public QueryResult Update(string foodId, string ingredientId, string unitId, double amount);
    }
}
