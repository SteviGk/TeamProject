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
    public class PassengersController : Controller
    {
        // GET: Passengers
        private DbManager db = new DbManager();
        public ActionResult Index()
        {
            var passengers = db.GetPassengers();
            return View(passengers);
        }

        public ActionResult Delete(string id)
        {
            ApplicationUser user = db.GetPassenger(id);          
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            db.DeletePassenger(id);
            return RedirectToAction("Index");
        }
    }
}