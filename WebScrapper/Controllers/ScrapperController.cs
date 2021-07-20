using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScrapper.Models;

namespace WebScrapper.Controllers
{
    public class ScrapperController : Controller
    {
        public IActionResult Index(UrlModel model)
        {
            if (!model.validateUrl(model))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = model.url;
            
            ImageScrapperModel image = new ImageScrapperModel();
            ViewBag.Images = image.getImages(model);

            WordScrapperModel word = new WordScrapperModel();
            ViewBag.Words = word.getWords(model);

            return View();
        }

    }
}
