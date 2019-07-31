using BusMeApp.Managers;
using BusMeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusMeApp.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        private DbManager db = new DbManager();
        public ActionResult Index()
        {
            var cities = db.GetCities().AsEnumerable();
            ViewBag.FromCityId = new SelectList(cities, "CityName", "CityName");
            ViewBag.ToCityId = new SelectList(cities, "CityName", "CityName");
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            var busRoutes = db.SearchBusRoute(vm.Departure, vm.FromCityId.ToString(), vm.ToCityId.ToString());
            return View();
        }
    }
}