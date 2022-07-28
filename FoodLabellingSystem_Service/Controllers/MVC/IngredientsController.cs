using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    [Route("/[controller]")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            Ingredients ingredients = await _ingredientService.GetAll();
            return View(ingredients.AllIngredients);
        }

        [HttpGet("Details/{ingredientId}")]
        public async Task<ActionResult> Details(string ingredientId)
        {
            Ingredient ingredient = await _ingredientService.GetById(ingredientId);
            if (ingredient == null) {
                return NotFound("The ingredient was not found."); 
            }
            return View(ingredient);
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredientController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ingredient ingredient)
        {
            if (ModelState.IsValid) {

                QueryResult queryResult = await _ingredientService.Add(ingredient);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("index");
                }
                else { 
                
                return View ("Error",new ErrorViewModel() { QureyResult = queryResult});
                }
            }
            
            return View();
         
        }

        [HttpGet("Edit/{ingredientId}")]
        public async Task<ActionResult> Edit(string ingredientId)
        {
            Ingredient ingredient = await _ingredientService.GetById(ingredientId);
            if (ingredient == null) {
                return NotFound("The ingredient was not found.");
            }
            return View(ingredient);
        }

        // POST: IngredientController/Edit/5
        [HttpPost("Edit/{ingredientId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Ingredient ingredient)
        {
            if (ModelState.IsValid) { 
            
                QueryResult queryResult = await _ingredientService.Update(ingredient);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("index");
                }
                else { 
                
                    return View("Error",new ErrorViewModel() { QureyResult = queryResult});
                }
            }
                return View();
            
        }

        [HttpGet("Delete/{ingredientId}")]
        public async Task<ActionResult> Delete(string ingredientId)
        {
            Ingredient ingredient = await _ingredientService.GetById(ingredientId);
            if (ingredient == null) {
                return NotFound("The ingredient was not found.");
            }
            return View(ingredient);
        }

        // POST: IngredientController/Delete/5
        [HttpPost("Delete/{ingredientId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmation(string ingredientId)
        {
           QueryResult queryResult = await _ingredientService.Remove(ingredientId);
            if (queryResult.Result == QueryResultType.SUCCEED) { 
            return RedirectToAction("index");
            }  
            return View("Error",new ErrorViewModel() { QureyResult = queryResult});
            
        }
    }
}
