using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    [Route("/[controller]")]
    public class CuisineTypesController : Controller
    {
        private readonly ICuisineTypeService _cuisineTypeService;
        private readonly ILogger<CuisineTypesController> _logger;
        public CuisineTypesController(ILogger<CuisineTypesController> logger, ICuisineTypeService cusineTypeService) {
            _logger = logger;
            _cuisineTypeService = cusineTypeService;
        }
        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            CuisineTypes cuisineTypes = await _cuisineTypeService.GetAll();
            return View(cuisineTypes.AllCuisineTypes);
        }

        [HttpGet("Details/{cuisineTypeId}")]
        public async Task<ActionResult> Details(string cuisineTypeId)
        {
            CuisineType cuisineType = await _cuisineTypeService.GetById(cuisineTypeId);
            return View(cuisineType);
        }

        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuisineTypeController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CuisineType cuisineType)
        {
            if (ModelState.IsValid)
            {

                QueryResult queryResult = await _cuisineTypeService.Add(cuisineType);
                if (queryResult.Result != QueryResultType.SUCCEED)
                {
                 //   _logger.LogError(DateTime.UtcNow.ToString() + " user{UserName}, Description {message}", queryResult.Message);
                    return View("Error", new ErrorViewModel() {QureyResult = queryResult} );
                }
                else {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View();
        }

       [HttpGet("Edit/{cuisineTypeId}")]
        public async  Task<ActionResult> Edit(string cuisineTypeId)
        {
            CuisineType founded =  await _cuisineTypeService.GetById(cuisineTypeId);
            if (founded == null)
            {
                return NotFound("The cuisine type Could not be found");
            }
           
            return View(founded);
        }

        // POST: CuisineTypeController/Edit/5
        [HttpPost("Edit/{cuisineTypeId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CuisineType cuisineType)
        {
            if (ModelState.IsValid) {
               
                    QueryResult queryResult = await _cuisineTypeService.Update(cuisineType);
                    if (queryResult.Result != QueryResultType.SUCCEED)
                    {
                        return View("Error",new ErrorViewModel() {QureyResult = queryResult});
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
            }
            return View();
        }

        [HttpGet("Delete/{cuisineTypeId}")]
        // GET: CuisineTypeController/Delete/5
        public async Task<ActionResult> Delete(string cuisineTypeId)
        {
            CuisineType cuisineType = await _cuisineTypeService.GetById(cuisineTypeId);
            if (cuisineType == null) { 
            return NotFound("The cuisine type not found.");
            }
            return View(cuisineType);
        }

        // POST: CuisineTypeController/Delete/5
        [HttpPost("Delete/{cuisineTypeId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string cuisineTypeId)
        {
           QueryResult queryResult = await _cuisineTypeService.Remove(cuisineTypeId);
            if (queryResult.Result != QueryResultType.SUCCEED)
            {
                return View("Error", new ErrorViewModel() { QureyResult = queryResult });
            }
            return RedirectToAction("Index");
        }
    }
}
