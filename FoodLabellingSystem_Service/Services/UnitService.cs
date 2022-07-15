using System;
using System.Data.SqlClient;
using System.Linq;
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services.Interfaces;

namespace FoodLabellingSystem_Service.Services
{
    public class UnitService :IUnitService
    {
        private readonly IUnitDAO _unitDAO;

        public UnitService(IUnitDAO unitDAO)
        {
            _unitDAO = unitDAO;
        }
        public List<Unit> Filter(Func<Unit, bool> func) {

                return _unitDAO.getAll().Where(x => func(x)).ToList();           
            }

        public Task<Units> GetAll() {
            
                return Task.Run(()=>{

                    Units units = new Units(); 
                    
                    units.AllUnits = _unitDAO.getAll();
                return units;
                
            });
           
        }

        public Task<Unit> GetById(string unitId)
        {

            return Task.Run(() => {

                return _unitDAO.GetById(unitId);

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

