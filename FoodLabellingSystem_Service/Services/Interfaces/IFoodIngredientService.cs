using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface IFoodIngredientService
    {
        public Task<FoodIngredients> GetAll();
        public Task<FoodIngredient> GetById(string foodId, string ingredientId);
        public Task<QueryResult> Add(FoodIngredient foodIngredient);
        public Task<QueryResult> Update(FoodIngredient foodIngredient);
        public Task<QueryResult> Delete(string foodIngredientId, string ingredientId);
    }
}