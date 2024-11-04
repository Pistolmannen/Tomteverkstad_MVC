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
            if (formID == "searchTomtenissar") {        // söka på tomtenissar
                @ViewBag.name = form["Name"];
                DataTable TomtenissarData = tm.GetTomtenissarByName(@ViewBag.name);
                ViewBag.Tomtenissar = TomtenissarData;
            }
            else if(formID == "create") {               // skapa tomtenissar
                @ViewBag.name = form["Name"];
                String ID = form["CID"];
                String nuts = form["Nuts"];
                String raisin = form["Raisin"];
                if (@ViewBag.name.Count == 0 || ID == null || nuts == null || raisin == null)
                {
                    ViewBag.updateStatus = "create needs all values";
                }
                else
                {
                    int.TryParse(nuts, out int intNuts);
                    int.TryParse(raisin, out int intRaisin);
                    tm.CreateTomtenisse(@ViewBag.name, ID, intNuts, intRaisin);
                    ViewBag.createStatus = "create might be successful";
                }
            }
            else if (formID == "update") {              // uppdatera tomtenissars sko storlek
                @ViewBag.name = form["Name"];
                String ID = form["ChefID"];
                String ShoeSize = form["ShoeSize"];
                if (@ViewBag.name.Count == 0 || ID == null || ShoeSize == null) {
                    ViewBag.updateStatus = "update needs all values";
                }
                else {
                    if (ShoeSize == "none")
                    {
                        tm.MakeShoeSizeNull(@ViewBag.name, ID);
                    }
                    else
                    {
                        tm.UpdateShoeSize(ShoeSize, @ViewBag.name, ID);
                    }
                    ViewBag.updateStatus = "update might be successful";
                }  

            }
            else if (formID == "searchLeksaker") {          // söka på leksaker beroned av namn
                @ViewBag.Prise = form["Prise"];
                int.TryParse(@ViewBag.Prise, out int prise);
                LeksakModel Lm = new LeksakModel(_configuration);
                DataTable LeksakData = Lm.GetleksakByPrise(prise);
                @ViewBag.leksaker = LeksakData; 
            }

            return View();
        }

        public IActionResult DeleteTomtenisse(String Name, String PNR)      // tabort tomtenissar
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