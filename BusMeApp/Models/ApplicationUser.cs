using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BusMeApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "You must input the first name of the passenger.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use only letters.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "You must input the last name of the passenger.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use only letters.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DisplayName("Identity Card")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only numbers and letters are allowed.")]
        [MinLength(8, ErrorMessage = "The identity card must contain only 8 letters.")]
        [MaxLength(8, ErrorMessage = "The identity card must contain only 8 letters.")]
        public string IdentityCard { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}