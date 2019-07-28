using BusMeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeApp.Models
{
    public class BusRoute
    {
        internal ICollection<ApplicationUser> Passengers;

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public RouteType Type { get; set; }

        [Required]
        [ForeignKey("From")]
        public int FromCityId { get; set; }

        public virtual City From { get; set; }

        [Required]
        [ForeignKey("To")]
        public int ToCityId { get; set; }
        public virtual City To { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int AvailableSeats { get; set; }

        [Required]
        public int RemainingSeats { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public BusRoute()
        {
            Reservations = new HashSet<Reservation>();
        }

    }
}