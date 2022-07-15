using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface IIngredientService
    {
        public Task<List<Ingredient>> GetAll();
        public Task<Ingredient> GetById(string ingredientId);
        public Task<QueryResult> Add(Ingredient ingredient);
        public Task<QueryResult> Remove(string ingredientId);
        public Task<QueryResult> Update(Ingredient ingredient);
    }
}
