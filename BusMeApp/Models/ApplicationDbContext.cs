using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BusMeApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BusRoute> BusRoutes { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany<BusRoute>(s => s.BusRoutes)
                .WithMany(c => c.Passengers)
                .Map(cs =>
                {
                    cs.MapLeftKey("ApplicationUserId");
                    cs.MapRightKey("BusRouteId");
                    cs.ToTable("Reservations");
                });
        }
    }
}