using BusMeApp.Models;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BusMeApp.Models
{   
    public class BusRoute
    {       
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must input departure time and date."),DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}",ApplyFormatInEditMode =true)]
        public DateTime Departure { get; set; }

        [GreaterThan("Departure")]
        [Required(ErrorMessage = "You must input arrival time and date."), DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}",ApplyFormatInEditMode =true)]     
        public DateTime Arrival { get; set; }
    
        [Required]
        public RouteType Type { get; set; }

        [ForeignKey("From")]
        [Required]      
        [DisplayName("From City")]
        public int FromCityId { get; set; }

        public virtual City From { get; set; }

        [ForeignKey("To")]
        [Required]
        [DisplayName("To City")]
        [NotEqualTo("FromCityId", ErrorMessage = "City names must be different")]
        public int ToCityId { get; set; }
        public virtual City To { get; set; }

        [Required(ErrorMessage = "You must input a price.")]
        [Range(0, 200, ErrorMessage = "Please enter a price between 0 and 200")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Available Seats")]

        [Range(1, 50, ErrorMessage = "Please enter available seats between 1 and 50")]
        public int AvailableSeats { get; set; }

        [LessThanOrEqualTo("AvailableSeats")]
        [Required]
        [DisplayName("Remaining Seats")]
        [Range(1, 50, ErrorMessage = "Please enter available seats between 1 and 50")]
        public int RemainingSeats { get; set; }

        public ICollection<ApplicationUser> Passengers;
    }
}