using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class FoodIngredientService : IFoodIngredientService

    {
        private readonly IFoodIngredientDAO _foodIngredientDAO;

        public FoodIngredientService(IFoodIngredientDAO foodIngredientDAO)
        {
            _foodIngredientDAO = foodIngredientDAO;
        }

        public Task<QueryResult> Add(FoodIngredient foodIngredient)
        {
                      return Task.Run(() => {
              return _foodIngredientDAO.Add(foodIngredient.FoodId, 
                  foodIngredient.IngredientId, foodIngredient.UnitId, foodIngredient.Amount,
                  foodIngredient.Fat, foodIngredient.Carbs, foodIngredient.Protein,
                  foodIngredient.Calory);
               
            });
        }

        public Task<QueryResult> Delete(string foodIngredientId, string ingredientId)
        {
            return Task.Run(() => {

                return _foodIngredientDAO.Remove(foodIngredientId, ingredientId);
            });
        }

        public Task<List<FoodIngredient>> GeAll()
        {
            return Task.Run(() => {
                return _foodIngredientDAO.GetAll();
            });
        }

        public Task<FoodIngredient> GetById(string foodIngredientId,string ingredientId)
        {
            return Task.Run(() => { 
            return _foodIngredientDAO.GetById(foodIngredientId, ingredientId);
            });
        }

        public Task<QueryResult> Update(FoodIngredient foodIngredient)
        {
            return Task.Run(() => {
               return _foodIngredientDAO.Update(foodIngredient.FoodId,
                   foodIngredient.IngredientId, foodIngredient.UnitId,
                   foodIngredient.Amount, foodIngredient.Fat, 
                   foodIngredient.Carbs, foodIngredient.Protein, foodIngredient.Calory);
            });
        }

    }
}
