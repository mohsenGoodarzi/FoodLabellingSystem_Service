using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IWarningDAO
    {
        public List<Warning> getAll();
        public Warning getById(string id);
        public QueryResult Add(string warningId, string message, string warningType);
        public QueryResult Remove(string WarningId);
        public QueryResult Update(string warningId, string message, string warningType);
    }
}