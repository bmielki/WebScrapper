using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using WebScrapper.Models;

namespace WebScrapper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        [HttpGet]
            public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UrlModel model)
        {
            if (!model.validateUrl(model)) {
                ModelState.Clear();
                ViewBag.Message = "Url inválida.";
                return View();
            }
            return RedirectToAction("Index", "Scrapper", model);
        }


        public IActionResult Contact() {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
