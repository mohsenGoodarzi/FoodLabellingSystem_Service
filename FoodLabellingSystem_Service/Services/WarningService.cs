using System;
using System.Data.SqlClient;
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class WarningService :IWarningService
    {
        public readonly IWarningDAO _warningDAO;

        public WarningService( IWarningDAO warningDAO)
        {
            _warningDAO = warningDAO;
        }

        public Task<List<Warning>> GetAll() {

          return Task.Run(()=>{
                {
                    List<Warning> warnings = _warningDAO.getAll();
                  
                    return warnings;
                };
            });
        }
        public Task<QueryResult> Add(Warning warning)
        {
           return  Task.Run(() => {
                return _warningDAO.Add(warning.WarningId, warning.Message, warning.WarningType);
            });
                
        }
        public Task<QueryResult> Remove(string warningId)
        {
           return Task.Run(() => {
                return _warningDAO.Remove(warningId);
            });
        }
        public Task<QueryResult> Update(Warning warning) {

               return Task.Run(() =>
                {
                    return _warningDAO.Update(warning.WarningId, warning.Message, warning.WarningType);
                });
        }

        public Task<Warning> GetById(string warningId)
        {
            return Task.Run(() => {
                return _warningDAO.getById(warningId);
            });
        }
    }
}

