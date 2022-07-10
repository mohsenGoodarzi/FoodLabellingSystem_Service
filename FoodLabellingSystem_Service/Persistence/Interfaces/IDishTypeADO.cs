﻿using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using System;
using System.Data.SqlClient;

namespace FoodLabellingSystem_Service.Persistence.Interfaces
{
    public interface IDishTypeADO
    {
        public List<DishType> GetAll();
        public QueryResult Add(string dishTypeId, string member);
        public QueryResult Remove(string dishTypeId);
        public QueryResult Update(string dishTypeId, string member);
    }
}

