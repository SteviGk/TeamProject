using BusMeApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Web.Mvc;

namespace BusMeApp.Managers
{
    public class DbManager
    {
        #region Cities
        public ICollection<City> GetCities()
        {
            ICollection<City> cities;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                cities = db.Cities.ToList();
            }
            return cities;
        }

        public City GetCity(int id)
        {
            City city;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                city = db.Cities.Find(id);
            }
            return city;
        }

        public void AddCity(City city)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Cities.Add(city);
                db.SaveChanges();
            }
        }

        public void UpdateCity(City city)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Cities.Attach(city);
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCity(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                City city = db.Cities.Find(id);
                db.Cities.Remove(city);
                db.SaveChanges();
            }
        }
        #endregion

        #region Passengers
        public ICollection<ApplicationUser> GetPassengers()
        {
            ICollection<ApplicationUser> passengers;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                passengers = db.Users.Where(x => x.UserName != "admin@busmeapp.com").ToList();
            }
            return passengers;
        }

        public ApplicationUser GetPassenger(string id)
        {
            ApplicationUser user;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                user = db.Users.Find(id);
            }
            return user;
        }

        public void DeletePassenger(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
        #endregion

        #region Reservations
        public ICollection<Reservation> GetReservations()
        {
            ICollection<Reservation> reservations;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservations = db.Reservations.Include("Passenger").Include("Route").Include("Route.From").Include("Route.To").ToList();
            }
            return reservations;
        }


        public ICollection<Reservation> GetReservations(string id)
        {
            ICollection<Reservation> reservations;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservations = db.Reservations.Where(x => x.Passenger.UserName == id).Include("Passenger").Include("Route").Include("Route.From").Include("Route.To").ToList();
            }
            return reservations;
        }

        public Reservation GetReservation(int id)
        {
            Reservation reservation;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservation = db.Reservations
                                .Include("Route")
                                .SingleOrDefault(i => i.Id == id);
            }

            return reservation;
        }

        public bool AddReservation(Reservation reservation, string name)
        {
            bool flag = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                BusRoute busRoute = db.BusRoutes.Find(reservation.BusRouteId);
                busRoute.RemainingSeats -= reservation.NumberOfTickets;
                ApplicationUser user = db.Users.FirstOrDefault(x => x.UserName == name);
                if (busRoute.RemainingSeats >= 0)
                {
                    reservation.PassengerId = user.Id;
                    reservation.TotalPrice = busRoute.Price * reservation.NumberOfTickets;
                    db.Reservations.Add(reservation);
                    db.BusRoutes.Attach(busRoute);
                    db.Entry(busRoute).State = EntityState.Modified;
                    db.SaveChanges();
                    flag = true;
                }

            }
            return flag;
        }

        public int TotalSeatsValueRollback(int id)
        {
            int result = 0;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Reservation reservation = db.Reservations.Find(id);
                result = reservation.NumberOfTickets;
            }
            return result;
        }

        //public bool UpdateReservation(Reservation reservation, string name)
        //{
        //    bool flag = false;
        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        int prevReservedSeats = TotalSeatsValueRollback(reservation.Id);
        //        BusRoute busRoute = db.BusRoutes.Find(reservation.BusRouteId);         
        //        busRoute.RemainingSeats = busRoute.RemainingSeats - reservation.NumberOfTickets + prevReservedSeats;
        //        ApplicationUser user = db.Users.FirstOrDefault(x => x.UserName == name);
        //        if (busRoute.RemainingSeats >= 0)
        //        {
        //            reservation.PassengerId = user.Id;
        //            reservation.TotalPrice = busRoute.Price * reservation.NumberOfTickets;
        //            db.BusRoutes.Attach(busRoute);
        //            db.Entry(busRoute).State = EntityState.Modified;
        //            db.Reservations.Attach(reservation);
        //            db.Entry(reservation).State = EntityState.Modified;
        //            db.SaveChanges();
        //            flag = true;
        //        }
        //    }
        //    return flag;
        //}

        public void DeleteReservation(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Reservation reservation = db.Reservations.Find(id);
                BusRoute busRoute = db.BusRoutes.Find(reservation.BusRouteId);
                busRoute.RemainingSeats += reservation.NumberOfTickets;
                db.Reservations.Remove(reservation);
                db.BusRoutes.Attach(busRoute);
                db.Entry(busRoute).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void ReservationPaymentCompleted(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var reservation = db.Reservations.Find(id);
                reservation.PaymentCompleted = true;
                db.SaveChanges();
            }
        }
        #endregion

        #region BusRoutes

        public ICollection<BusRoute> SearchBusRoute(DateTime departure, int fromCityId, int toCityId)
        {
            ICollection<BusRoute> busRoutes;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                busRoutes = db.BusRoutes
                    .Where(i=> i.Departure.Year == departure.Year && i.Departure.Month == departure.Month && i.Departure.Day == departure.Day && i.FromCityId == fromCityId && i.ToCityId == toCityId)
                    .Include("From")
                    .Include("To")
                    .ToList();
            }
            return busRoutes;
        }
        public ICollection<BusRoute> GetBusRoutes()
        {
            ICollection<BusRoute> busRoutes;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                busRoutes = db.BusRoutes
                              .Include("From")
                              .Include("To")
                              .ToList();
            }
            return busRoutes;
        }

        public BusRoute GetBusRoute(int id)
        {
            BusRoute busRoute;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                busRoute = db.BusRoutes.Find(id);
            }
            return busRoute;
        }

        public void AddBusRoute(BusRoute busRoute)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.BusRoutes.Add(busRoute);
                db.SaveChanges();
            }
        }

        public void UpdateBusRoute(BusRoute busRoute)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.BusRoutes.Attach(busRoute);
                db.Entry(busRoute).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteBusRoute(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                BusRoute busRoute = db.BusRoutes.Find(id);
                db.BusRoutes.Remove(busRoute);
                db.SaveChanges();
            }
        }
        #endregion

        #region Chat
        public ICollection<ApplicationUser> GetUsers(string name)
        {
            ICollection<ApplicationUser> users;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (name == "admin@busmeapp.com")
                {
                    users = db.Users.Where(x => x.UserName != name).ToList();
                }
                else
                {
                    users = db.Users.Where(x => x.UserName == "admin@busmeapp.com").ToList();
                }
            }
            return users;
        }

        public void AddPost(Post post)
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (!string.IsNullOrEmpty(post.Text))
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                }
            }
        }
       
        public ICollection<Post> GetPosts(string from,string to)
        {
            ICollection<Post> posts;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser userFrom = db.Users.FirstOrDefault(x => x.UserName == from);
                ApplicationUser userTo= db.Users.FirstOrDefault(x => x.UserName == to);
                posts = db.Posts.Where(x => x.FromUserId == userFrom.Id && x.ToUserId == userTo.Id).ToList();
            }
            return posts;
        }

        #endregion
    }
}