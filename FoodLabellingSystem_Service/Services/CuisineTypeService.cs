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

        public Task<List<CuisineType>> GetAll() {
            return Task.Run(() => { 
            List<CuisineType> cusineTypes = _cuisineTypeDAO.GetAll();
                return cusineTypes;
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
                _cuisineTypeDAO.Add(cuisineType.CuisineTypeId,cuisineType.Member);
                return queryResult;
            });

        }

        public Task<QueryResult> Update(CuisineType cuisineType) {
            QueryResult queryResult = new QueryResult();
            return Task.Run(() => {
                _cuisineTypeDAO.Update(cuisineType.CuisineTypeId, cuisineType.Member);
                return queryResult;
            });
        }
        public Task<QueryResult> Remove(string cuisineTypeId)
        {
            QueryResult queryResult = new QueryResult();
            return Task.Run(() => {
                _cuisineTypeDAO.Remove(cuisineTypeId);
                return queryResult;
            });
        }
    }
}
