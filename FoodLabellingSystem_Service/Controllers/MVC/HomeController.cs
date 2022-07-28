using FoodLabellingSystem_Service.Models;
using FoodLabellingSystem_Service.Other;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodLabellingSystem_Service.Controllers.MVC
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("/Error/{errorViewModel}")]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            return View(errorViewModel);
        }
    }
}