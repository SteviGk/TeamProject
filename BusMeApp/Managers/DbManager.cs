using BusMeApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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

        public void UpdatePassenger(ApplicationUser user,string name,string password)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                user.UserName = name;
                user.Email = name;
                user.PasswordHash = password;
                db.Users.Attach(user);
                db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();          
            }
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
                reservations = db.Reservations.ToList();
            }
            return reservations;
        }


        public ICollection<Reservation> GetReservations(string id)
        {
            ICollection<Reservation> reservations;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservations = db.Reservations.Where(x=>x.Passenger.UserName==id).ToList();
            }
            return reservations;
        }

        public Reservation GetReservation(int id)
        {
            Reservation reservation;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                reservation = db.Reservations.Find(id);
            }
            return reservation;
        }

        public bool AddReservation(Reservation reservation,string name)
        {
            bool flag = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                BusRoute busRoute = db.BusRoutes.Find(reservation.BusRouteId);
                busRoute.RemainingSeats -= reservation.NumberOfTickets;
                ApplicationUser user = db.Users.FirstOrDefault(x=>x.UserName==name);
                if (busRoute.RemainingSeats >= 0)
                {
                    reservation.PassengerId = user.Id;
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

        public bool UpdateReservation(Reservation reservation,string name)
        {
            bool flag = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                int prevReservedSeats = TotalSeatsValueRollback(reservation.Id);
                BusRoute busRoute = db.BusRoutes.Find(reservation.BusRouteId);
                busRoute.RemainingSeats= busRoute.RemainingSeats - reservation.NumberOfTickets + prevReservedSeats;
                ApplicationUser user = db.Users.FirstOrDefault(x => x.UserName == name);
                if (busRoute.RemainingSeats >= 0)
                {
                    reservation.PassengerId = user.Id;
                    db.BusRoutes.Attach(busRoute);
                    db.Entry(busRoute).State = EntityState.Modified;
                    db.Reservations.Attach(reservation);
                    db.Entry(reservation).State = EntityState.Modified;
                    db.SaveChanges();
                    flag = true;
                }
            }
            return flag;
        }

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
        #endregion

        #region BusRoutes

        public ICollection<BusRoute> SearchBusRoute(DateTime departure, string fromCityId, string toCityId)
        {
            ICollection<BusRoute> busRoutes;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                busRoutes = db.BusRoutes.Where(i=> i.Departure.ToShortDateString() == departure.ToShortDateString() && i.FromCityId == fromCityId && i.ToCityId == toCityId).ToList();
            }
            return busRoutes;
        }
        public ICollection<BusRoute> GetBusRoutes()
        {
            ICollection<BusRoute> busRoutes;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                busRoutes = db.BusRoutes.ToList();
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
    }
}