using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface IUnitService
    {
        public List<Unit> Filter(Func<Unit, bool> func);
        public Task<Units> GetAll();
        public Task<Unit> GetById(string unitId);
        public Task<QueryResult> Add(Unit unit);
        public Task<QueryResult> Remove(string unitId);
        public Task<QueryResult> Update(Unit unit);
    }
}

