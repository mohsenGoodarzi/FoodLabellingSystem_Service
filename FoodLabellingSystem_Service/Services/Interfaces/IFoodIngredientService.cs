using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface IFoodIngredientService
    {
        public Task<List<FoodIngredient>> GeAll();
        public Task<FoodIngredient> GetById(string foodIngredientId, string ingredientId);
        public Task<QueryResult> Add(FoodIngredient foodIngredient);
        public Task<QueryResult> Update(FoodIngredient foodIngredient);
        public Task<QueryResult> Delete(string foodIngredientId, string ingredientId);
    }
}