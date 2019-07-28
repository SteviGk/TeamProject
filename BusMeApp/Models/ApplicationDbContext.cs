using BusMeApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;


namespace BusMeApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BusRoute> BusRoutes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany<BusRoute>(s => s.BusRoutes)
            //    .WithMany(c => c.Passengers)
            //    .Map(cs =>
            //    {
            //        cs.MapLeftKey("ApplicationUserId");
            //        cs.MapRightKey("BusRouteId");
            //        cs.ToTable("Reservations");
            //    });
        }
    }
}