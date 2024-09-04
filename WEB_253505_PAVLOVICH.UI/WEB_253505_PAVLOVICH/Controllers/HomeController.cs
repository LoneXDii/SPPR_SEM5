using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253505_PAVLOVICH.HelpClasses;
using WEB_253505_PAVLOVICH.Models;

namespace WEB_253505_PAVLOVICH.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewData["Header"] = "Лабораторная работа 2";

            var listData = new List<ListDemo> { 
                new ListDemo {Id=1, Name="Item 1"},
                new ListDemo {Id=2, Name="Item 2"},
                new ListDemo {Id=3, Name="Item 3"}
            };

            var model = new IndexViewModel { 
                ListItems = new SelectList(listData, "Id", "Name")
            };

            return View(model);
        }
    }
}
