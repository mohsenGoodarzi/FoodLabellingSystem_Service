using FoodLabellingSystem_Service.Other;
using System.Data.SqlClient;
namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IUnitDAO
    {
        public SqlDataReader? getAll();
        public QueryResult Add(string unitId, double toGram);
        public QueryResult Remove(string unitId);
        public QueryResult Update(string unitId, double toGram);
    }
}

