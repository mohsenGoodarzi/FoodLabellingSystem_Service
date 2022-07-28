using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services.Interfaces
{
    public interface IWarningService
    {
        public Task<Warnings> GetAll();
        public Task<Warning> GetById(string warningId);
        public Task<QueryResult> Add(Warning warning);
        public Task<QueryResult> Remove(string warningId);
        public Task<QueryResult> Update(Warning warning);

    }
}

