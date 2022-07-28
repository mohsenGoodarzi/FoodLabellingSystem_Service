
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class DishTypeService : IDishTypeService
    {
        private IDishTypeDAO _dishTypeDAO;
        public DishTypeService(IDishTypeDAO dishTypeDAO)
        {
            _dishTypeDAO = dishTypeDAO;
        }


        public Task<QueryResult> Add(DishType dishType)
        {
            QueryResult queryResult = new QueryResult();
            return Task.Run(() => {
                queryResult = _dishTypeDAO.Add(dishType.DishTypeId, dishType.Member);
                return queryResult;
            });
        }

        public Task<QueryResult> Delete(string dishTypeId)
        {
            QueryResult queryResult = new QueryResult();
            return Task.Run(() => {
                queryResult = _dishTypeDAO.Remove(dishTypeId);
                return queryResult;
            });
        }

        public Task<DishTypes> GeAll()
        {
            return Task.Run(() => {
                DishTypes dishTypes = new DishTypes();
                dishTypes.AllDishTypes = _dishTypeDAO.GetAll();
                return dishTypes;
            });
        }

        public Task<DishType> GetById(string dishTypeId)
        {
            return Task.Run(() => {
                DishType dishType = _dishTypeDAO.GetById(dishTypeId);
                return dishType;
            });
        }

        public Task<QueryResult> Update(DishType dishType)
        {
            QueryResult queryResult = new QueryResult();
            return Task.Run(() => {
                queryResult = _dishTypeDAO.Update(dishType.DishTypeId,dishType.Member);
                return queryResult;
            });
        }
    }
}
