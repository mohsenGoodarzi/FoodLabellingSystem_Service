using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    [Route("/[controller]")]
    public class UnitsController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }


        // GET: UnitController
        [HttpGet("Index")]
        public async Task<ActionResult> Index()
        {
            Units units = await _unitService.GetAll();
            
            return View(units.AllUnits);
        }

        [HttpGet("Details/{unitId}")]
        // GET: UnitController/Details/5
        public async Task<ActionResult> Details(string unitId)
        {
            Unit unit = await _unitService.GetById(unitId);
            if (unit == null) { 
            return NotFound("unit not found");
            }
            return View(unit);
        }

        [HttpGet("Create")]
        // GET: UnitController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Unit unit)
        {
            if (ModelState.IsValid) {
               QueryResult queryResult = await _unitService.Add(unit);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction(nameof(Index));

                }
                else { 
                return View(new ErrorViewModel(){ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, QureyResult = queryResult});
                }
            
            }
   
                return View();
            
        }

        [HttpGet("Edit/{unitId}")]
        // GET: UnitController/Edit/5
        public async Task<ActionResult> Edit(string unitId)
        {
            Unit unit = await _unitService.GetById(unitId);
            if (unit == null) { 
            return NotFound("Unit not found.");
            }
            return View(unit);
        }

        // POST: UnitController/Edit/5
        [HttpPost("Edit/{unitId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                QueryResult queryResult = await _unitService.Update(unit);
                if (queryResult.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, QureyResult = queryResult });
                }
            }           
                return View();
        }

        [HttpGet("Delete/{unitId}")]
        // GET: UnitController/Delete/5
        public async Task<ActionResult> Delete(string unitId)
        {
            Unit unit = await _unitService.GetById(unitId);
            if (unit == null) { 
            return NotFound("Unit not found.");
            }
            return View(unit);
        }

        // POST: UnitController/Delete/5
        [HttpPost("Delete/{unitId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string unitId)
        {
            QueryResult queryResult = await _unitService.Remove(unitId);
            if (queryResult.Result == QueryResultType.SUCCEED)
            {
                return RedirectToAction("Index");

            }
            else {
                return View(new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, QureyResult = queryResult });
            }

        }
    }
}
