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
                  foodIngredient.IngredientId, foodIngredient.UnitId, foodIngredient.Amount);
               
            });
        }

        public Task<QueryResult> Delete(string foodId, string ingredientId)
        {
            return Task.Run(() => {

                return _foodIngredientDAO.Remove(foodId, ingredientId);
            });
        }

        public Task<FoodIngredients> GetAll()
        {
            return Task.Run(() => {
                FoodIngredients foodIngredients = new FoodIngredients();
                foodIngredients.AllFoodIngredients = _foodIngredientDAO.GetAll();
                return foodIngredients;
            });
        }

        public Task<FoodIngredient> GetById(string foodId,string ingredientId)
        {
            return Task.Run(() => { 
            return _foodIngredientDAO.GetById(foodId, ingredientId);
            });
        }

        public Task<QueryResult> Update(FoodIngredient foodIngredient)
        {
            return Task.Run(() => {
               return _foodIngredientDAO.Update(foodIngredient.FoodId,
                   foodIngredient.IngredientId, foodIngredient.UnitId,
                   foodIngredient.Amount);
            });
        }

    }
}
