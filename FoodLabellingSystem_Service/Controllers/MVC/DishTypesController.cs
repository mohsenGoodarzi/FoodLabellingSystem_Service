using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    [Route("/[controller]")]
    public class DishTypesController : Controller
    {
        private readonly IDishTypeService _dishTypeService;
        public DishTypesController(IDishTypeService dishTypeService)
        {
            _dishTypeService = dishTypeService;
        }

        [HttpGet("GetAll")]
        // GET: DishTypeController
        public async Task<ActionResult> Index()
        {
           DishTypes dishTypes = await _dishTypeService.GeAll();
            return View(dishTypes.AllDishTypes);
        }

        [HttpGet("Get/{dishTypeId}")]
        // GET: DishTypeController/Details/5
        public async Task<ActionResult> Details(string dishTypeId)
        {
            DishType dishType = await _dishTypeService.GetById(dishTypeId);

            if (dishType == null) {
                return NotFound("Dish type not found");
            }
            return View(dishType);
        }

        // GET: DishTypeController/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: DishTypeController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DishType dishType)
        {
            if (ModelState.IsValid) {
               QueryResult queryResult = await _dishTypeService.Add(dishType);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("GetAll");
                }
                else {
                    return View("Error", new ErrorViewModel() { QureyResult = queryResult });
                }
            }
            
                return View();
 
        }


        [HttpGet("Edit/{dishTypeId}")]
        // GET: DishTypeController/Edit/5
        public async Task<ActionResult> Edit(string dishTypeId)
        {
           DishType dishType = await _dishTypeService.GetById(dishTypeId);
            if (dishType == null) {
                return NotFound("Dish type not found");
            }
            return View(dishType);
        }

        // POST: DishTypeController/Edit/5
        [HttpPost("Edit/{dishTypeId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DishType dishType)
        {
            if (ModelState.IsValid) {
                QueryResult queryResult = await _dishTypeService.Update(dishType);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("GetAll");
                }
                else {
                
                    return View("Error", new ErrorViewModel() { QureyResult = queryResult});
                }
            }
            
                return View();
            
        }

        [HttpGet("Remove/{dishTypeId}")]
        // GET: DishTypeController/Delete/5
        public async Task<ActionResult> Delete(string dishTypeId)
        {
           DishType dishType = await _dishTypeService.GetById(dishTypeId);
            if (dishType == null) { 
            return NotFound("Dish type not found.");
            }

            return View(dishType);
        }

        // POST: DishTypeController/Delete/5
        [HttpPost("Remove/{dishTypeId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmation(string dishTypeId)
        {
           QueryResult queryResult = await _dishTypeService.Delete(dishTypeId);
            if (queryResult.Result == QueryResultType.SUCCEED)
            {
                return RedirectToAction("GetAll");
            }
            return View("Error",new ErrorViewModel() {QureyResult = queryResult});
        }
    }
}
