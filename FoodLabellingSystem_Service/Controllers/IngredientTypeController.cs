using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodLabellingSystem_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientTypeController : ControllerBase
    {
        private readonly IIngredientTypeService _ingredientTypeService;
        public IngredientTypeController(IIngredientTypeService ingredientTypeService) { 
        _ingredientTypeService = ingredientTypeService;
        }

        // GET: api/<IngredientTypeController>
        [HttpGet("GetAll")]
        public async Task<List<IngredientType>> GetAll()
        {
            return await _ingredientTypeService.GetAll();
                
        }

        // GET api/<IngredientTypeController>/5
        [HttpGet("Get/{ingredientTypeId}")]
        public async Task<IngredientType> Get(string ingredientTypeId)
        {
            return await _ingredientTypeService.GetById(ingredientTypeId);
        }

        // POST api/<IngredientTypeController>
        [HttpPost("Post")]
        public async Task<QueryResult> Post([FromBody] IngredientType ingredientType)
        {
            return await _ingredientTypeService.Add(ingredientType);

        }

        // PUT api/<IngredientTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IngredientTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
