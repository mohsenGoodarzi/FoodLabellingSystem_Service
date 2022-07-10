using System;
using System.Data.SqlClient;
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class UnitService :IUnitService
    {
        private readonly IUnitDAO _unitDAO;

        public UnitService(IUnitDAO unitDAO)
        {
            _unitDAO = unitDAO;
        }

        public Task<List<Unit>> GetAll() {
            
                return Task.Run(()=>{

                List<Unit> units = new List<Unit>();

                SqlDataReader? dataReader = _unitDAO.getAll();
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        // ToGram column id is 1.
                        units.Add(new Unit(dataReader["UnitId"].ToString(), dataReader.GetDouble(1)));
                    }

                }
                return units;
            });
           
        }

        public Task<QueryResult> Add(Unit unit) {

            return Task.Run(()=>{
                return _unitDAO.Add(unit.UnitId, unit.ToGram);
            });
        }

        public Task<QueryResult> Remove(string unitId) {
            return Task.Run(() => {
                return _unitDAO.Remove(unitId);
            });
           
        }

        public Task<QueryResult> Update(Unit unit) {

            return Task.Run(() => {
                return _unitDAO.Update(unit.UnitId, unit.ToGram);
            });
            

        }
    }
}

