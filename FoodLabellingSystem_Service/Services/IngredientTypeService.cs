
using System.Data.SqlClient;
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class IngredientTypeService:IIngredientTypeService
    {
      
        private readonly IIngredientTypeDAO ingredientTypeDAO;

        public IngredientTypeService(IIngredientTypeDAO ingredientTypeDAO)
        {
            this.ingredientTypeDAO = ingredientTypeDAO;
        }

        public Task<QueryResult> Add(IngredientType ingredientType)
        {
            return Task.Run(() => {
               return ingredientTypeDAO.Add(ingredientType.IngredientTypeId, ingredientType.Member);
            });
            
        }

        public Task<List<IngredientType>> GetAll()
        {
            return Task.Run(() => {
                List<IngredientType> ingredientTypes = ingredientTypeDAO.GetAll();
                return ingredientTypes;
            });
            
        }
        public Task<IngredientType> GetById(string ingredientTypeId)
        {
            return Task.Run(() => {
            
            return ingredientTypeDAO.GetById(ingredientTypeId);
            });
        }
        public Task<QueryResult> Remove(string ingredientTypeId)
        {
            return Task.Run(() => {
                return ingredientTypeDAO.Remove(ingredientTypeId);
            });
            }


        public Task<QueryResult> Update(IngredientType ingredientType)
        {
            return Task.Run(() => { 
            return ingredientTypeDAO.Update(ingredientType.IngredientTypeId, ingredientType.Member);
            }); }

        
    }
}

