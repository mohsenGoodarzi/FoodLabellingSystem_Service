using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface IFoodService
    {
        public Task<List<Food>> GeAll();
        public Task<Food> GetById(string foodId);
        public Task<QueryResult> Add(Food food);
        public Task<QueryResult> Update(Food food);
        public Task<QueryResult> Delete(string foodId);

    }
}
