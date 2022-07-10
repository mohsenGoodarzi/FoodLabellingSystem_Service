
using System.Data.SqlClient;
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class IngredientTypeService:IIngredientTypeService
    {
      
        private readonly IIngredientTypeDAO ingredientTypeDAO;

        public IngredientTypeService(IIngredientTypeDAO ingredientTypeDAO)
        {
            this.ingredientTypeDAO = ingredientTypeDAO;
        }

        public QueryResult Add(IngredientType ingredientType)
        {
            return ingredientTypeDAO.Add(ingredientType.IngredientTypeId, ingredientType.Member);
        }

        public List<IngredientType> GetAll()
        {
            List<IngredientType> ingredientTypes = new List<IngredientType>();
            SqlDataReader? dataReader = ingredientTypeDAO.GetAll();
            if (dataReader != null) {
                while (dataReader.Read()) {

                    ingredientTypes.Add(new IngredientType());
                }
            }
            return ingredientTypes;
        }

       public QueryResult Remove(string ingredientTypeId)
        {
            return ingredientTypeDAO.Remove(ingredientTypeId);
        }


        public QueryResult Update(IngredientType ingredientType)
        {
            return ingredientTypeDAO.Update(ingredientType.IngredientTypeId, ingredientType.Member);
        }

        
    }
}

