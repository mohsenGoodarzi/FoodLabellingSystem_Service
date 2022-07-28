using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    [Route("/[controller]")]
    public class FoodsController : Controller
    {
        private readonly IFoodService _foodService;
        public FoodsController(IFoodService foodService) {
            _foodService = foodService;
        }
       [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
           Foods foods= await _foodService.GetAll();
            return View(foods.AllFoods);
        }

        [HttpGet("Details/{foodId}")]
        public async Task<ActionResult> Details(string foodId)
        {
            Food food= await _foodService.GetById(foodId);
            if (food == null) {
                return NotFound("The food not found");
            }
            return View(food);
        }

       [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,dishType,cuisineType,FoodType")]Food food)
        {
            if (ModelState.IsValid) {
                QueryResult queryResult = await _foodService.Add(food);
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

        // GET: FoodController/Edit/5
        [HttpGet("Edit/{foodId}")]
        public async Task<ActionResult> Edit(string foodId)
        {
           Food food = await _foodService.GetById(foodId);
            if (food == null) {
                return NotFound("The food not found");
            }
            return View(food);
        }

        // POST: FoodController/Edit/5
        [HttpPost("Edit/{foodId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Name,dishType,cuisineType,FoodType")] Food food)
        {
            if (ModelState.IsValid) {

              QueryResult queryResult = await  _foodService.Update(food);
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
        [HttpGet("Delete/{foodId}")]
        public async Task<ActionResult> Delete(string foodId)
        {
            Food food = await _foodService.GetById(foodId);
            if (food == null) { 
            return NotFound("The food was not found");
            }
            return View(food);
        }

        // POST: FoodController/Delete/5
        [HttpPost("Delete/{foodId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmation(string foodId)
        {
            QueryResult queryResult = await _foodService.Delete(foodId);
            if (queryResult.Result == QueryResultType.SUCCEED)
            {
                return RedirectToAction("Index");
            }
            return View("Error", new ErrorViewModel() { QureyResult = queryResult });
        }
    }
}
