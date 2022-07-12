using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Persistence.Interfaces;
using FoodLabellingSystem_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodLabellingSystem_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUnitService _unitService;
        public UnitController(IConfiguration configuration, IUnitService unitService) { 
        
            _configuration = configuration;
            _unitService = unitService;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        [Route("GetAlL")]
        public async Task<Units> Get()
        {

            return await _unitService.GetAll();
        }

        // GET api/<ValuesController>/5
        [HttpGet("GetById/{id}")]
        public async Task<Unit> Get(string id)
        {

            Unit unitA = new Unit("Gram", 1);
            Unit unitB = new Unit("Gram", 1);

            if (unitA == unitB) {
                Console.WriteLine("They are equal");
            }

            return await _unitService.GetById(id);
        }

        // POST api/<ValuesController>
        [HttpPost("Create")]
        public async Task<QueryResult> Post([FromBody] Unit unit)
        {
            return await _unitService.Add(unit);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("Update/{id}")]
        public async Task<QueryResult> Put([FromBody] Unit unit)
        {
            return await _unitService.Update(unit);
        }

        // DELETE api/<ValuesController>/5

        [HttpDelete("Delete/{id}")]
        public async Task<QueryResult> Delete(string id)
        {
            return await _unitService.Remove(id);
        }
    }
}
