using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface IDishTypeService
    {
        public Task<DishTypes> GeAll();
        public Task<DishType> GetById(string dishTypeId);
        public Task<QueryResult> Add(DishType dishType);
        public Task<QueryResult> Update(DishType dishType);
        public Task<QueryResult> Delete(string dishTypeId);



    }
}
