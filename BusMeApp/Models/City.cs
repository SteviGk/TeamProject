using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusMeApp.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use only letters.")]
        [StringLength(30,MinimumLength =2)]
        [Display(Name ="City Name")]
        public string CityName { get; set; }
    }
}