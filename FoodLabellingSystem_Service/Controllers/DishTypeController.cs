using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodLabellingSystem_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishTypeController : ControllerBase
    {
        private IDishTypeService _dishTypeService;
        public DishTypeController(IDishTypeService dishTypeService) { 
        _dishTypeService = dishTypeService; 
        }
        // GET: api/<DishTypeController>
        [HttpGet("GetAll")]
        public async Task<List<DishType>> GetAll()
        {
            return await _dishTypeService.GeAll();
        }

        // GET api/<DishTypeController>/5
        [HttpGet("Get/{id}")]
        public async Task<DishType> Get(string dishType)
        {
            return await _dishTypeService.GetById(dishType);
        }

        // POST api/<DishTypeController>
        [HttpPost("Post")]
        public async Task<QueryResult> Post([FromBody] DishType dishType)
        {
            return await _dishTypeService.Add(dishType);
        }

        // PUT api/<DishTypeController>/5
        [HttpPut("Put")]
        public async Task<QueryResult> Put([FromBody] DishType dishType)
        {
            return await _dishTypeService.Update(dishType);
        }

        // DELETE api/<DishTypeController>/5
        [HttpDelete("Delete{dishTypeId}")]
        public async Task<QueryResult> Delete(string dishTypeId)
        {
           return await _dishTypeService.Delete(dishTypeId);
        }
    }
}
