using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class FoodService:IFoodService
    {
        private readonly IFoodDAO _foodDAO;

        public FoodService(IFoodDAO foodDAO) { 
        
            _foodDAO = foodDAO;
        }
        public Task<Foods> GetAll()
        {
            return Task.Run(() => {
                Foods foods = new Foods();
                foods.AllFoods =_foodDAO.GetAll();
                return foods; 
            });
         }

        public Task<Food> GetById(string foodId)
        {
            return Task.Run(() => {
                Food food = _foodDAO.GetById(foodId);
            return food;
            }); 
       }

        public Task<QueryResult> Add(Food food)
        {
           QueryResult result = new QueryResult();
            return Task.Run(() => {
                result = _foodDAO.Add(food.FoodId, food.Description, food.DishType, food.CuisineType, food.FoodType);
                return result;
            });
        }

        public Task<QueryResult> Update(Food food)
        {
            QueryResult result = new QueryResult();
            return Task.Run(() => {
                result = _foodDAO.Update(food.FoodId, food.Description, food.DishType, food.CuisineType, food.FoodType);
                return result;
            });
        }

        public Task<QueryResult> Delete(string foodId)
        {
            QueryResult result = new QueryResult();
            return Task.Run(() => {
                result = _foodDAO.Remove(foodId);
                return result;
            });
        } 
    }
}
