namespace BusMeApp.Migrations
{
    using BusMeApp.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<BusMeApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BusMeApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

//            SeedIdentities(context);
//        }

//        internal static void SeedIdentities(ApplicationDbContext context)
//        {
//            try
//            {
//                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
//                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

//                if (!roleManager.RoleExists("Administrator"))
//                {
//                    var roleresult = roleManager.Create(new IdentityRole("Administrator"));
//                }

//                ApplicationUser admin = userManager.FindByName("admin@busmeapp.com");

//                if (admin == null)
//                {
//                    admin = new ApplicationUser()
//                    {
//                        UserName = "admin@busmeapp.com",
//                        Email = "admin@busmeapp.com",
//                        EmailConfirmed = true,
//                        FirstName = "Admin",
//                        LastName = "Adminomanolakis",
//                        IdentityCard = "AA000000"
//                    };
//                    IdentityResult userResult = userManager.Create(admin, "!admin1@");

//                    if (userResult.Succeeded)
//                    {
//                        var result = userManager.AddToRole(admin.Id, "Administrator");
//                    }
//                }

//                if (!context.Cities.Any())
//                {
//                    context.Cities.Add(new City { CityName = "Athens" });
//                    context.Cities.Add(new City { CityName = "Arta" });
//                    context.Cities.Add(new City { CityName = "Ioannina" });
//                    context.SaveChanges();
//                }


//            }
//            catch (DbEntityValidationException ex)
//            {
//                StringBuilder sb = new StringBuilder();

//                foreach (var failure in ex.EntityValidationErrors)
//                {
//                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
//                    foreach (var error in failure.ValidationErrors)
//                    {
//                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
//                        sb.AppendLine();
//                    }
//                }

//                throw new DbEntityValidationException(
//                    "Entity Validation Failed - errors follow:\n" +
//                    sb.ToString(), ex
//                ); // Add the original exception as the innerException
//            }

//        }
//    }
//}
