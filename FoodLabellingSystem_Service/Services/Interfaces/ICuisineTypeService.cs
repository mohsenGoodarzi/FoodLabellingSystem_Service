using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface ICuisineTypeService
    {
        public Task<CuisineTypes> GetAll();
        public Task<CuisineType> GetById(string cuisineTypeId);
        public Task<QueryResult> Add(CuisineType cuisineType);
        public Task<QueryResult> Update(CuisineType cuisineType);
        public Task<QueryResult> Remove(string cuisineTypeId);

    }
}