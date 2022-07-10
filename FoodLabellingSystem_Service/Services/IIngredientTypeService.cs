
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services
{
    public interface IIngredientTypeService
    {
        public List<IngredientType> GetAll();
        public QueryResult Add(IngredientType ingredientType);
        public QueryResult Remove(string ingredientTypeId);
        public QueryResult Update(IngredientType ingredientType);
    }
}

