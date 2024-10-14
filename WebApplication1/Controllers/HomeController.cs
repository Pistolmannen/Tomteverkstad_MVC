using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tomteverkstad(IFormCollection form)
        {
            TomtenisseModel tm = new TomtenisseModel(_configuration);
            DataTable AllTomtenissarData = tm.GetTomtenissar();
            ViewBag.AllTomtenissar = AllTomtenissarData;

            string formID = form["formID"];
            if (formID == "login") {
                @ViewBag.print = "login";
            }
            else if (formID == "search") {
                @ViewBag.print = "search";
                @ViewBag.name = form["Name"];
                DataTable TomtenissarData = tm.GetTomtenissarByName(@ViewBag.name);
                ViewBag.Tomtenissar = TomtenissarData;

            }
            else if(formID == "create") {
                @ViewBag.print = "create";
                @ViewBag.name = form["Name"];
            }
            else if (formID == "update") {
                @ViewBag.print = "update";
                @ViewBag.name = form["Name"];
                @ViewBag.ID = form["ChefID"];
                @ViewBag.ShoeSize = form["ShoeSize"];
                if (@ViewBag.name.Count == 0 || @ViewBag.ID.Count == 0 || @ViewBag.ShoeSize.Count == 0) {
                    ViewBag.updateStatus = "update needs all values";
                }
                else {
                    if (form["Shoesize"] == "none")
                    {
                        tm.MakeShoeSizeNull(form["Name"], form["ChefID"]);
                    }
                    else
                    {
                        tm.UpdateShoeSize(form["Shoesize"], form["Name"], form["ChefID"]);
                    }
                    ViewBag.updateStatus = "update successful";
                }  

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