using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IFoodDAO
    {
        public List<Food> GetAll();
        public Food GetById(string id);
        public QueryResult Add(string foodId, string description, string dishType, string cuisineType, string foodType, string userName);
        public QueryResult Remove(string foodId);
        public QueryResult Update(string foodId, string description, string dishType, string cuisineType, string foodType, string userName);
    }
}
