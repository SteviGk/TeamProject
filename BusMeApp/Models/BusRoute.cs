using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusMeApp.Models
{
    public class BusRoute
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public DateTime Arrival { get; set; }
        // Type enum ???????
        //from to fk ξεχωριστός πίνακας

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<ApplicationUser> Passengers { get; set; }

        public BusRoute()
        {
            Passengers = new HashSet<ApplicationUser>();
        }


    }
}