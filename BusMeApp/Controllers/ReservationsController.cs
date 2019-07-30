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
    public class ReservationsController : Controller
    {
        // GET: Reservations
        private DbManager db = new DbManager();

        public ActionResult Index()
        {
            if (!User.IsInRole("Administrator"))
            {
                string name = User.Identity.Name;
                var reservations = db.GetReservations(name);
                return View(reservations);
            }
            else
            {
                var reservations = db.GetReservations();
                return View(reservations);
            }
        }

        public ActionResult Create(int id)
        {
            if (User.IsInRole("Administrator"))
            {
                return RedirectToAction("Index");
            }

            ViewBag.PassengerId = new SelectList(db.GetPassengers(), "Id", "Id");
            ViewBag.BusRouteId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PassengerId = new SelectList(db.GetPassengers(), "Id", "Id");
                ViewBag.BusRouteId = new SelectList(db.GetBusRoutes(), "Id", "Id");
                return View(reservation);
            }
            string name = User.Identity.Name;
            if (db.AddReservation(reservation, name))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("No more available seats");
            }
        }

        public ActionResult Edit(int id)
        {
            Reservation reservation = db.GetReservation(id);
            ViewBag.PassengerId = new SelectList(db.GetPassengers(), "Id", "Id");
            ViewBag.BusRouteId = new SelectList(db.GetBusRoutes(), "Id", "Id");
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PassengerId = new SelectList(db.GetPassengers(), "Id", "Id");
                ViewBag.BusRouteId = new SelectList(db.GetBusRoutes(), "Id", "Id");
                return View(reservation);
            }
            string name = User.Identity.Name;
            if (db.UpdateReservation(reservation, name))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return Content("No more available seats");
            }
        }

        public ActionResult Delete(int id)
        {
            Reservation reservation = db.GetReservation(id);
            ViewBag.PassengerId = new SelectList(db.GetPassengers(), "Id", "Id");
            ViewBag.BusRouteId = new SelectList(db.GetBusRoutes(), "Id", "Id");
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteReservation(id);
            return RedirectToAction("Index");
        }
    }
}