using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Bloomfiy_final.Models;



[assembly: OwinStartup(typeof(Bloomfiy_final.Startup))]

namespace Bloomfiy_final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);        // auth & cookies
            CreateRolesAndAdmin();     // roles + admin + seed data
        }

        private void CreateRolesAndAdmin()
        {
            using (var context = new ApplicationDbContext())
            {
                // ===== ROLES =====
                var roleManager = new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                    roleManager.Create(new IdentityRole("Admin"));

                if (!roleManager.RoleExists("User"))
                    roleManager.Create(new IdentityRole("User"));

                // ===== ADMIN USER =====
                var userManager = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

                var adminEmail = "admin@bloomfiy.com";
                var adminUser = userManager.FindByEmail(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        FirstName = "Admin",
                        LastName = "Bloomfiy"
                    };

                    userManager.Create(adminUser, "Admin@12345");
                    userManager.AddToRole(adminUser.Id, "Admin");
                }

                // ✅ ADD CATEGORIES
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new[]
                    {
                new Categories { CategoryName = "Roses" },
                new Categories { CategoryName = "Tulips" },
                new Categories { CategoryName = "Bouquets" },
                new Categories { CategoryName = "Seasonal Flowers" }
            });

                    context.SaveChanges();
                }

                // ✅ ADD COLORS
                if (!context.Colors.Any())
                {
                    context.Colors.AddRange(new[]
                    {
                new Color { ColorName = "Red", ColorCode = "#b11226", PriceAdjustment = 0 },
                new Color { ColorName = "Pink", ColorCode = "#f4a7b9", PriceAdjustment = 5 },
                new Color { ColorName = "White", ColorCode = "#ffffff", PriceAdjustment = 3 },
                new Color { ColorName = "Yellow", ColorCode = "#f7d046", PriceAdjustment = 4 }
            });

                    context.SaveChanges();
                }
            }
        }
    }
}
