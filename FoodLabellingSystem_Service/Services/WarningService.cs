using System;
using System.Data.SqlClient;
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;

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
                    List<Warning> warnings = new List<Warning>();
                    SqlDataReader? dataReader = _warningDAO.getAll();

                    if (dataReader != null)
                    {

                        while (dataReader.Read())
                        {
                            warnings.Add(new Warning(warningId: dataReader["warningId"].ToString(), dataReader["Message"].ToString(), dataReader["WarningType"].ToString()));
                        }
                    }
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
    }
}

