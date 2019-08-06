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

        public ActionResult Edit(string id)
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
        public ActionResult Edit(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            string name = user.UserName;
            string password = user.PasswordHash;
            db.UpdatePassenger(user, name, password);
            return RedirectToAction("Index");
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