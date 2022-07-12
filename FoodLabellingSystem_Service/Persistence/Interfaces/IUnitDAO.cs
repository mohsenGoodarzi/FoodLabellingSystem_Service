using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using System.Data.SqlClient;
namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IUnitDAO
    {
        public List<Unit> getAll();
        public Unit GetById(string id);
        public QueryResult Add(string unitId, double toGram);
        public QueryResult Remove(string unitId);
        public QueryResult Update(string unitId, double toGram);
    }
}

