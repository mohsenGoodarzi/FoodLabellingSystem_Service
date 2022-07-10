﻿
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Services
{
    public interface IWarningService
    {
        public Task<List<Warning>> GetAll();
        public Task<QueryResult> Add(Warning warning);
        public Task<QueryResult> Remove(string warningId);
        public Task<QueryResult> Update(Warning warning);

    }
}

