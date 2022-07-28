using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodLabellingSystem_Service.Controllers.API
{
    [Route("/api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {

            _ingredientService = ingredientService;
        }
        // GET: api/<IngredientController>
        [HttpGet("GetAll")]
        public async Task<Ingredients> GetAll()
        {
            return await _ingredientService.GetAll();
        }

        // GET api/<IngredientController>/5
        [HttpGet("Get/{ingredientId}")]
        public async Task<Ingredient> Get(string ingredientId)
        {
            return await _ingredientService.GetById(ingredientId);
        }

        // POST api/<IngredientController>
        [HttpPost("Post")]
        public async Task<QueryResult> Post([FromBody] Ingredient ingredient)
        {
            return await _ingredientService.Add(ingredient);
        }

        // PUT api/<IngredientController>/5
        [HttpPut("Put")]
        public async Task<QueryResult> Put([FromBody] Ingredient ingredient)
        {
            return await _ingredientService.Update(ingredient);
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("Delete/{ingredientId}")]
        public async Task<QueryResult> Delete(string ingredientId)
        {
            return await _ingredientService.Remove(ingredientId);
        }
    }
}
