using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusMeApp.ViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "From")]
        [Required(ErrorMessage = "You must select departure city.")]
        public int FromCityId { get; set; }

        [Display(Name = "To")]
        [Required(ErrorMessage = "You must select arrival city.")]
        [NotEqualTo("FromCityId", ErrorMessage = "City names must be different")]
        public int ToCityId { get; set; }

        [Display(Name = "Departure")]
        [Required(ErrorMessage = "You must input departure date.")]
        public DateTime Departure { get; set; }
    }
}