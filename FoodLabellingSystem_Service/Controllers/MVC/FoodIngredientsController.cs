using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    [Route("/[controller]")]
    public class FoodIngredientsController : Controller
    {
        private readonly IFoodIngredientService _foodIngredientService;
        private readonly IUnitService _unitService;
        public FoodIngredientsController(IFoodIngredientService foodIngredientService, IUnitService unitService ) {
            _foodIngredientService = foodIngredientService;
            _unitService = unitService;
        }
        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            FoodIngredients foodIngredients = await _foodIngredientService.GetAll();
            return View(foodIngredients.AllFoodIngredients);
        }

        [HttpGet("Details/{foodId}/{ingredientId}")]
        public async Task<ActionResult> Details(string foodId, string ingredientId)
        {
            FoodIngredient foodIngredient = await _foodIngredientService.GetById(foodId, ingredientId);
            if (foodIngredient == null) {
                return NotFound("Food ingredient was not found.");
            }
            
            return View(foodIngredient);
        }

       [HttpGet("Create")]
        public async Task<ActionResult> Create()
        {
            Units units = await _unitService.GetAll();
            ViewData["Units"] = new SelectList(units.AllUnits, "UnitId", "UnitId","gram");

            return View();
        }

       
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FoodIngredient foodIngredient)
        {
            if (ModelState.IsValid) {
                QueryResult queryResult = await _foodIngredientService.Add(foodIngredient);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", new ErrorViewModel() { QureyResult = queryResult });
                
                }
            }
            return View();
        }

        [HttpGet("Edit/{foodId}/{ingredientId}")]
        public async Task<ActionResult> Edit(string foodId, string ingredientId)
        {
            FoodIngredient foodIngredient = await _foodIngredientService.GetById(foodId, ingredientId);
            if (foodIngredient == null) {

                return NotFound("The food ingredient was not found.");
            }
            Units units = await _unitService.GetAll();
            ViewData["Units"] = new SelectList(units.AllUnits, "UnitId", "UnitId");
            return View(foodIngredient);
        }

  
        [HttpPost("Edit/{foodId}/{ingredientId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FoodIngredient foodIngredient)
        {
           
            if (ModelState.IsValid) {
                QueryResult queryResult = await _foodIngredientService.Update(foodIngredient);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", new ErrorViewModel() { QureyResult = queryResult });
                }
            }
           return View();
        }

       [HttpGet("Delete/{foodId}/{ingredientId}")]
        public async Task<ActionResult> Delete(string foodId, string ingredientId)
        {
            FoodIngredient foodIngredient = await _foodIngredientService.GetById(foodId, ingredientId);

            if (foodIngredient == null) {
                return NotFound("The food ingredient was not found.");
            } 
            return View(foodIngredient);
        }

        // POST: FoodIngredientController/Delete/5
        [HttpPost("Delete/{foodId}/{ingredientId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string foodId, string ingredientId)
        {
          QueryResult queryResult = await _foodIngredientService.Delete(foodId, ingredientId);

            if (queryResult.Result == QueryResultType.SUCCEED) { 
            return RedirectToAction("Index");
            }
                return View("Error", new ErrorViewModel() { QureyResult = queryResult});
        }
    }
}
