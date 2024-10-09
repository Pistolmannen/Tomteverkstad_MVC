using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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

        public IActionResult Tomteverkstad(IFormCollection form)
        {
            string formID = form["formID"];
            if (formID == "login") {
                @ViewBag.print = "login";
            }
            else if (formID == "search") {
                @ViewBag.print = "search";
                @ViewBag.name = form["Name"];
            }
            else if(formID == "create") {
                @ViewBag.print = "create";
                @ViewBag.name = form["Name"];
            }
            else if (formID == "update") {
                @ViewBag.print = "update";
                @ViewBag.name = form["Name"];
            }
            else {
                @ViewBag.print = "none";
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}