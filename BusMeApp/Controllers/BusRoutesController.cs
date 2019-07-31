using BusMeApp.Managers;
using BusMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusMeApp.Controllers
{
    [Authorize]
    public class BusRoutesController : Controller
    {
        // GET: BusRoutes
        private DbManager db = new DbManager();
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var busRoutes = db.GetBusRoutes();
            return View(busRoutes);
        }

        public ActionResult Details()
        {
            if (!User.IsInRole("Admin"))
            {
                var busRoutes = db.GetBusRoutes();
                return View(busRoutes);
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var cities = db.GetCities().AsEnumerable();
            ViewBag.FromCityId = new SelectList(cities, "Id", "CityName");
            ViewBag.ToCityId = new SelectList(cities, "Id", "CityName");
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusRoute busRoute)
        {
            if (!ModelState.IsValid)
            {
                var cities = db.GetCities().AsEnumerable();
                ViewBag.FromCityId = new SelectList(cities, "Id", "CityName");
                ViewBag.ToCityId = new SelectList(cities, "Id", "CityName");
                return View(busRoute);
            }
            db.AddBusRoute(busRoute);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            BusRoute busRoute = db.GetBusRoute(id);
            var cities = db.GetCities().AsEnumerable();
            ViewBag.FromCityId = new SelectList(cities, "Id", "CityName", busRoute.FromCityId);
            ViewBag.ToCityId = new SelectList(cities, "Id", "CityName", busRoute.ToCityId);
            if (busRoute == null)
            {
                return HttpNotFound();
            }
            return View(busRoute);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusRoute busRoute)
        {
            if (!ModelState.IsValid)
            {
                var cities = db.GetCities().AsEnumerable();
                ViewBag.FromCityId = new SelectList(cities, "Id", "CityName");
                ViewBag.ToCityId = new SelectList(cities, "Id", "CityName");
                return View(busRoute);
            }
            db.UpdateBusRoute(busRoute);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            BusRoute busRoute = db.GetBusRoute(id);
            var cities = db.GetCities().AsEnumerable();
            ViewBag.FromCityId = new SelectList(cities, "Id", "CityName");
            ViewBag.ToCityId = new SelectList(cities, "Id", "CityName");
            if (busRoute == null)
            {
                return HttpNotFound();
            }
            return View(busRoute);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteBusRoute(id);
            return RedirectToAction("Index");
        }
    }
}