using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusMeApp.Models;

namespace BusMeApp.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Passenger")]
        public string PassengerId { get; set; }

        public virtual ApplicationUser Passenger { get; set; }

        [ForeignKey("Route")]
        public int BusRouteId { get; set; }
        public virtual BusRoute Route { get; set; }
        public int NumberOfTickets { get; set; }
    }
}