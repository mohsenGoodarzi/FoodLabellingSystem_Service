using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodLabellingSystem_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuisineTypeController : ControllerBase
    {
        private readonly ICuisineTypeService _cuisineTypeService;
        public CuisineTypeController(ICuisineTypeService cuisineTypeService) {
            _cuisineTypeService = cuisineTypeService;
        }
        // GET: api/<CuisineTypeController>
        [HttpGet("GetAll")]
        public async Task<List<CuisineType>> GetAll()
        {
            return await _cuisineTypeService.GetAll();
        }

        // GET api/<CuisineTypeController>/5
        [HttpGet("Get/{cuisineTypeId}")]
        public async Task<CuisineType> Get(string cuisineTypeId)
        {
            return await _cuisineTypeService.GetById(cuisineTypeId);
        }

        // POST api/<CuisineTypeController>
        [HttpPost("Post")]
        public async Task<QueryResult> Create([FromBody] CuisineType cuisineType)
        {
            return await _cuisineTypeService.Add(cuisineType);
        }

        // PUT api/<CuisineTypeController>/5
        [HttpPut("Put")]
        public async Task<QueryResult> Put([FromBody] CuisineType cuisineType)
        {
            return await _cuisineTypeService.Update(cuisineType);
        }

        // DELETE api/<CuisineTypeController>/5
        [HttpDelete("Delete/{id}")]
        public async Task<QueryResult> Delete(string cuisineTypeId)
        {
            return await _cuisineTypeService.Remove(cuisineTypeId);
        }
    }
}
