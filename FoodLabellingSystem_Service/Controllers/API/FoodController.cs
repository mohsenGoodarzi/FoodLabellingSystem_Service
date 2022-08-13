using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodLabellingSystem_Service.Controllers.API
{
    [Route("/api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;
        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }
        // GET: api/<FoodController>
        [HttpGet("GetAll")]
        public async Task<Foods> GetAll()
        {
            return await _foodService.GetAll();
        }

        // GET api/<FoodController>/5
        [HttpGet("Get/{id}")]
        public async Task<Food> Get(string foodId)
        {
            return await _foodService.GetById(foodId);
        }

        // POST api/<FoodController>
        [HttpPost("Post")]
        public async Task<QueryResult> Post([FromBody] Food food)
        {
            return await _foodService.Add(food,"mgdana");
        }

        // PUT api/<FoodController>/5
        [HttpPut("Put")]
        public async Task<QueryResult> Put([FromBody] Food food)
        {
            return await _foodService.Update(food, "mgdana");
        }

        // DELETE api/<FoodController>/5
        [HttpDelete("Delete{foodId}")]
        public async Task<QueryResult> Delete(string foodId)
        {
            return await _foodService.Delete(foodId);
        }
    }
}
