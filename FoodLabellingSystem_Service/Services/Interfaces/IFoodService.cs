using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface IFoodService
    {
        public Task<Foods> GetAll();
        public Task<Food> GetById(string foodId);
        public Task<QueryResult> Add(Food food,string userName);
        public Task<QueryResult> Update(Food food, string userName);
        public Task<QueryResult> Delete(string foodId);

    }
}
