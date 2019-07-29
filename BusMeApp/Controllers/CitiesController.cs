using BusMeApp.Managers;
using BusMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusMeApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CitiesController : Controller
    {
        private DbManager db = new DbManager();

        // GET: Cities
        public ActionResult Index()
        {
            var cities = db.GetCities();
            return View(cities);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(City city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }
            db.AddCity(city);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            City city = db.GetCity(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            if (!ModelState.IsValid)
            {
                return View(city);
            }
            db.UpdateCity(city);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            City city = db.GetCity(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteCity(id);
            return RedirectToAction("Index");
        }
    }
}