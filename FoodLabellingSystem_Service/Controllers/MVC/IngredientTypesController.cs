
using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    [Route("/[controller]")]
    public class IngredientTypesController : Controller
    {
        private readonly IIngredientTypeService _ingredientTypeService;
        public IngredientTypesController(IIngredientTypeService ingredientTypeService ) {
            _ingredientTypeService = ingredientTypeService;
        }
        
        
        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            IngredientTypes ingredientTypes = await _ingredientTypeService.GetAll();
            return View(ingredientTypes.AllIngredientTypes);
        }

        [HttpGet("Details/{ingredientTypeId}")]
        public async Task<ActionResult> Details(string ingredientTypeId)
        {
            IngredientType ingredientType = await _ingredientTypeService.GetById(ingredientTypeId);
          if(ingredientType == null){
                return NotFound("Ingredient type was not found.");
            }
            return View(ingredientType);
        }

        [HttpGet("Create")]
        public  ActionResult Create()
        {
            return View();
        }

        // POST: IngredientTypeController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IngredientType ingredientType)
        {
            if (ModelState.IsValid) {

                QueryResult queryResult = await _ingredientTypeService.Add(ingredientType);

            }
                return View();
           
        }

        [HttpGet("Edit/{ingredientTypeId}")]
        public async Task<ActionResult> Edit(string ingredientTypeId)
        {
            IngredientType ingredientType = await _ingredientTypeService.GetById(ingredientTypeId);
            if (ingredientType == null) {
                return NotFound("Ingredient type was not found.");
            }
            return View(ingredientType);
        }

        // POST: IngredientTypeController/Edit/5
        [HttpPost("Edit/{ingredientTypeId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(IngredientType ingredientType)
        {
            if (ModelState.IsValid) {

               QueryResult queryResult = await _ingredientTypeService.Update(ingredientType);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("Index");
                }
                else {
                    return View("Error", new ErrorViewModel() { QureyResult = queryResult });
                }

            }
                return View();
        }

        [HttpGet("Delete/{ingredientTypeId}")]
        public async Task<ActionResult> Delete(string ingredientTypeId)
        {
            IngredientType ingredientType = await _ingredientTypeService.GetById(ingredientTypeId);
            if (ingredientType == null) {

                return NotFound("Ingredient type was not found"); 
            }
            return View(ingredientType);
        }

        // POST: IngredientTypeController/Delete/5
        [HttpPost("Delete/{ingredientTypeId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmation(string ingredientTypeId)
        {
            QueryResult queryResult = await _ingredientTypeService.Remove(ingredientTypeId);
            if (queryResult.Result == QueryResultType.SUCCEED) { 
            return RedirectToAction("Index");
            }
            return View("Error", new ErrorViewModel() { QureyResult = queryResult });
        }
    }
}
