using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using FoodLabellingSystem_Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    [Route("/[controller]")]
    public class WarningsController : Controller
    {
        private readonly IWarningService _warningService;
        public WarningsController(IWarningService warningService) { 
        
            _warningService = warningService;
        
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult> Index()
        {
            Warnings warnings =  await _warningService.GetAll();
            return View(warnings.AllWarnings);
        }

        [HttpGet("Details/{warningId}")]
        public async Task<ActionResult> Details(string warningId)
        {
            Warning warning = await _warningService.GetById(warningId);
            if (warning == null) { 
            return NotFound("The warning you are looking for does not exist.");
            }
           return View(warning);
        }
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("WarningId,Message,WarningType")] Warning warning)
        {
            if (ModelState.IsValid)
            {
                QueryResult result = await _warningService.Add(warning);
                if (result.Result == QueryResultType.SUCCEED)
                {
                    return RedirectToAction("GetAll", "/Warning");
                }
                else {
                    return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, QureyResult = result });
                }
            }

            return View();
        }
       
        [HttpGet("Edit/{warningId}")]
        public async Task<ActionResult> Edit(string warningId)
        {
            Warning warning = await _warningService.GetById(warningId);
            if (warning == null)
            {
                return NotFound();
            }
            return View(warning);
        }

        [HttpPost("Edit/{warningId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Warning warning)
        {
            if (ModelState.IsValid) {

                QueryResult result = await _warningService.Update(warning);
                if (result.Result == QueryResultType.SUCCEED)
                {

                    return RedirectToAction("GetAll", "/Warning");
                }
                else { 
                return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, QureyResult = result });
                
                }
                
            }

            return View();
        }

        [HttpGet("Remove/{warningId}")]
        public async Task<ActionResult> Remove(string warningId)
        {
            Warning warning = await _warningService.GetById(warningId);
            if (warning == null) { 
            return NotFound();
            }
            return View(warning);
        }

        // POST: WarningController/Delete/5
        [HttpPost("Remove/{warningId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveConfirmed(string warningId)
        {
            QueryResult result = await _warningService.Remove(warningId);

            if (result.Result == QueryResultType.SUCCEED)
            {

                return RedirectToAction("GetAll", "/Warning");
            }

            return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, QureyResult = result });
        }
    }
}
