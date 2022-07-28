using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface IIngredientTypeService
    {
        public Task<IngredientTypes> GetAll();
        public Task<IngredientType> GetById(string ingredientTypeId);
        public Task<QueryResult> Add(IngredientType ingredientType);
        public Task<QueryResult> Remove(string ingredientTypeId);
        public Task<QueryResult> Update(IngredientType ingredientType);
    }
}

