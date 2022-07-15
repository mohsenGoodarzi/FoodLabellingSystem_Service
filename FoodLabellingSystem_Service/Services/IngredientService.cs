using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientDAO _ingredientDAO;

        public IngredientService(IIngredientDAO ingredientDAO) { 
        _ingredientDAO = ingredientDAO;
        }
        public Task<QueryResult> Add(Ingredient ingredient)
        {
            return Task.Run(() => {
                return _ingredientDAO.Add(ingredient.IngredientId, ingredient.Description, ingredient.IngredientTypeId,
                    ingredient.UnitId, ingredient.Amount, ingredient.Fat, ingredient.Carbs, ingredient.Protein, ingredient.Calory, ingredient.WarningId);
            });
        }

        public Task<List<Ingredient>> GetAll()
        {
            return Task.Run(() => { 
            return _ingredientDAO.GetAll();
            });
        }

        public Task<Ingredient> GetById(string ingredientId)
        {
            return Task.Run(() => {
                return _ingredientDAO.GetById(ingredientId);
            });
        }

        public Task<QueryResult> Remove(string ingredientId)
        {
            return Task.Run(() => {
                return _ingredientDAO.Remove(ingredientId);
            
            });
        }

        public Task<QueryResult> Update(Ingredient ingredient)
        {
            return Task.Run(() => {
                return _ingredientDAO.Update(ingredient.IngredientId, 
                    ingredient.Description, ingredient.IngredientTypeId, 
                    ingredient.UnitId, ingredient.Amount, ingredient.Fat, 
                    ingredient.Carbs, ingredient.Protein, ingredient.Calory,
                    ingredient.WarningId);
            });
            
        }
    }
}
