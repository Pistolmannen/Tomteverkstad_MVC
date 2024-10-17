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
            else if (formID == "searchTomtenissar") {
                @ViewBag.name = form["Name"];
                DataTable TomtenissarData = tm.GetTomtenissarByName(@ViewBag.name);
                ViewBag.Tomtenissar = TomtenissarData;
            }
            else if(formID == "create") {
                @ViewBag.name = form["Name"];
                @ViewBag.ID = form["CID"];
                @ViewBag.nuts = form["Nuts"];
                @ViewBag.raisin = form["Raisin"];
                if (@ViewBag.name.Count == 0 || @ViewBag.ID.Count == 0 || @ViewBag.nuts.Count == 0 || @ViewBag.raisin.Count == 0)
                {
                    ViewBag.updateStatus = "create needs all values";
                }
                else
                {
                    int.TryParse(@ViewBag.nuts, out int nuts);
                    int.TryParse(@ViewBag.raisin, out int raisin);
                    tm.CreateTomtenisse(@ViewBag.name, @ViewBag.ID, nuts, raisin);
                    ViewBag.createStatus = "create might be successful";
                }
            }
            else if (formID == "update") {
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
                    ViewBag.updateStatus = "update might be successful";
                }  

            }
            else if (formID == "searchLeksaker") {
                @ViewBag.Prise = form["Prise"];
                int.TryParse(@ViewBag.Prise, out int prise);
                LeksakModel Lm = new LeksakModel(_configuration);
                DataTable LeksakData = Lm.GetleksakByPrise(prise);
                @ViewBag.leksaker = LeksakData; 
            }

            return View();
        }

        public IActionResult DeleteTomtenisse(String Name, String PNR) 
        { 
            TomtenisseModel tm = new TomtenisseModel(_configuration);   // test
            tm.DeleteTomtenisse(Name, PNR);                             // 000000-0000-1-000000000


            return RedirectToAction("Tomteverkstad", "Home"); 
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