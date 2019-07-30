using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusMeApp.ViewModels
{
    public class SearchViewModel
    {
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }

        public DateTime Departure { get; set; }
    }
}