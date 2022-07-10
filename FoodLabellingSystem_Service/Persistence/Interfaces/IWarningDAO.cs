using FoodLabellingSystem_Service.Other;
using System.Data.SqlClient;
namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IWarningDAO
    {
        public SqlDataReader? getAll();
        public QueryResult Add(string warningId, string message, string warningType);
        public QueryResult Remove(string WarningId);
        public QueryResult Update(string warningId, string message, string warningType);
    }
}