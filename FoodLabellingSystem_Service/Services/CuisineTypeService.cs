using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class CuisineTypeService : ICuisineTypeService
    {
        private readonly ICuisineTypeDAO _cuisineTypeDAO;

        public CuisineTypeService(ICuisineTypeDAO cuisineTypeDAO) { 
        _cuisineTypeDAO = cuisineTypeDAO;   
        }

        public Task<CuisineTypes> GetAll() {
            return Task.Run(() => { 
                 CuisineTypes cuisineTypes = new CuisineTypes();
                cuisineTypes.AllCuisineTypes = _cuisineTypeDAO.GetAll();
                return cuisineTypes;
            });
        }
        public Task<CuisineType> GetById(string cuisineTypeId) {

            return Task.Run(() => { 
            
            return _cuisineTypeDAO.GetById(cuisineTypeId);
            });
        }
        public Task<QueryResult> Add(CuisineType cuisineType) {
            QueryResult queryResult = new QueryResult();
            return Task.Run(() => {
                queryResult = _cuisineTypeDAO.Add(cuisineType.CuisineTypeId,cuisineType.Member);
                return queryResult;
            });

        }

        public Task<QueryResult> Update(CuisineType cuisineType) {
            QueryResult queryResult = new QueryResult();
            return Task.Run(() => {
                queryResult = _cuisineTypeDAO.Update(cuisineType.CuisineTypeId, cuisineType.Member);
                return queryResult;
            });
        }
        public Task<QueryResult> Remove(string cuisineTypeId)
        {
            QueryResult queryResult = new QueryResult();
            return Task.Run(() => {
                queryResult = _cuisineTypeDAO.Remove(cuisineTypeId);
                return queryResult;
            });
        }
    }
}
