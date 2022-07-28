using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodLabellingSystem_Service.Controllers.API
{
    [Route("/api/[controller]")]
    [ApiController]
    public class WarningController : ControllerBase
    {
        private readonly IWarningService _warningService;
        public WarningController(IWarningService warningService)
        {
            _warningService = warningService;
        }
        // GET: api/<WarningController>
        [HttpGet("Get/All")]
        public async Task<Warnings> GetAll()
        {
            return await _warningService.GetAll();
        }

        // GET api/<WarningController>/5
        [HttpGet("Get/{warningId}")]
        public async Task<Warning> Get(string warningId)
        {
            return await _warningService.GetById(warningId);
        }

        // POST api/<WarningController>
        [HttpPost("Post")]
        public async Task<QueryResult> Post([FromBody] Warning warning)
        {
            return await _warningService.Add(warning);
        }

        // PUT api/<WarningController>/5
        [HttpPut("Put")]
        public async Task<QueryResult> Put([FromBody] Warning warning)
        {
            return await _warningService.Update(warning);
        }

        // DELETE api/<WarningController>/5
        [HttpDelete("Delete/{warningId}")]
        public async Task<QueryResult> Delete(string warningId)
        {
            return await _warningService.Remove(warningId);
        }
    }
}
