using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodLabellingSystem_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodIngredientController : ControllerBase
    {
        private readonly FoodIngredientService _foodIngredientService;
        public FoodIngredientController(FoodIngredientService foodIngredientService) { 
        _foodIngredientService = foodIngredientService;
        }

        // GET: api/<FoodIngredientController>
        [HttpGet("GetAll")]
        public async Task<List<FoodIngredient>> Get()
        {
            return await _foodIngredientService.GeAll();
        }

        // GET api/<FoodIngredientController>/5
        [HttpGet("Get/{foodId}/{ingredientId}")]
        public async Task<FoodIngredient> Get(string foodId, string ingredientId)
        {
            return await _foodIngredientService.GetById(foodId,ingredientId);
        }

        // POST api/<FoodIngredientController>
        [HttpPost("Post")]
        public async Task<QueryResult> Post([FromBody] FoodIngredient foodIngredient)
        {
            return await _foodIngredientService.Add(foodIngredient);
        }

        // PUT api/<FoodIngredientController>/5
        [HttpPut("Put")]
        public async Task<QueryResult> Put([FromBody] FoodIngredient foodIngredient)
        {
           return await _foodIngredientService.Update(foodIngredient);
        }

        // DELETE api/<FoodIngredientController>/5
        [HttpDelete("Delete/{foodId}/{ingredientId}")]
        public async Task<QueryResult> Delete(string foodId, string ingredientdId)
        {
            return await _foodIngredientService.Delete(foodId, ingredientdId); 
        }
    }
}
