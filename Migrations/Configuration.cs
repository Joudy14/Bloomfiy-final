namespace Bloomfiy_final.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Bloomfiy_final.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Bloomfiy_final.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Bloomfiy_final.Models.ApplicationDbContext";
        }

        protected override void Seed(Bloomfiy_final.Models.ApplicationDbContext context)
        {
            // ---------------- COLORS ----------------
            context.Colors.AddOrUpdate(
                c => c.ColorName,
                new Color { ColorName = "Red", IsAvailable = true, ColorCode = "#FF0000" },
                new Color { ColorName = "Pink", IsAvailable = true, ColorCode = "#FFC0CB" },
                new Color { ColorName = "White", IsAvailable = true, ColorCode = "#FFFFFF" },
                new Color { ColorName = "Yellow", IsAvailable = true, ColorCode = "#FFD700" },

                new Color { ColorName = "Orange", IsAvailable = true, ColorCode = "#FFA500" },
                new Color { ColorName = "Purple", IsAvailable = true, ColorCode = "#800080" },
                new Color { ColorName = "Blue", IsAvailable = true, ColorCode = "#1E90FF" },
                new Color { ColorName = "Fuchsia", IsAvailable = true, ColorCode = "#FF00FF" }
            );



            // ---------------- CATEGORIES ----------------
            context.Categories.AddOrUpdate(
                c => c.CategoryName,
                new Categories { CategoryName = "Birthdays" },
                new Categories { CategoryName = "Valentine" },
                new Categories { CategoryName = "Weddings" },
                new Categories { CategoryName = "Graduation" },
                new Categories { CategoryName = "Anniversaries" },
                new Categories { CategoryName = "Special Occasions" }
                );
        }
    }
}
