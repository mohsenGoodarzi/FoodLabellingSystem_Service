
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services
{
    public interface IUnitService
    {
        public Task<List<Unit>> GetAll();
        public Task<QueryResult> Add(Unit unit);
        public Task<QueryResult> Remove(string unitId);
        public Task<QueryResult> Update(Unit unit);
    }
}

