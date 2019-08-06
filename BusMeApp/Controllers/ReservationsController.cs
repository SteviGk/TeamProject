using BusMeApp.Managers;
using BusMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            ViewBag.PassengerId = new SelectList(db.GetPassengers().AsEnumerable(), "Id", "Id");
            ViewBag.BusRouteId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PassengerId = new SelectList(db.GetPassengers().AsEnumerable(), "Id", "Id");
                ViewBag.BusRouteId = new SelectList(db.GetBusRoutes().AsEnumerable(), "Id", "Id");
                return View(reservation);
            }
            string name = User.Identity.Name;
            if (db.AddReservation(reservation, name))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        //public ActionResult Edit(int id)
        //{
        //    Reservation reservation = db.GetReservation(id);
        //    if (reservation == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(reservation);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Reservation reservation)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.PassengerId = new SelectList(db.GetPassengers().AsEnumerable(), "Id", "Id");
        //        ViewBag.BusRouteId = new SelectList(db.GetBusRoutes().AsEnumerable(), "Id", "Id");
        //        return View(reservation);
        //    }
        //    string name = User.Identity.Name;
        //    if (db.UpdateReservation(reservation, name))
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //}

        public ActionResult Delete(int id)
        {
            Reservation reservation = db.GetReservation(id);
            ApplicationUser user = db.GetPassenger(reservation.PassengerId);
            BusRoute busRoute = db.GetBusRoute(reservation.BusRouteId);
            City cityFrom = db.GetCity(busRoute.FromCityId);
            City cityTo = db.GetCity(busRoute.ToCityId);
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            ViewBag.IdenityCard= user.IdentityCard;
            ViewBag.FromCityId = cityFrom.CityName;
            ViewBag.ToCityId = cityTo.CityName;
            ViewBag.Departure = busRoute.Departure;
            ViewBag.Arrival = busRoute.Arrival;
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