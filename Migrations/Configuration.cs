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

                new Color { ColorName = "Red", IsAvailable = true, ColorCode = "#FF0000", PriceAdjustment = 0m },
                new Color { ColorName = "Pink", IsAvailable = true, ColorCode = "#FFC0CB", PriceAdjustment = 0m },
                new Color { ColorName = "White", IsAvailable = true, ColorCode = "#FFFFFF", PriceAdjustment = 0m },
                new Color { ColorName = "Yellow", IsAvailable = true, ColorCode = "#FFD700", PriceAdjustment = 0m },

                new Color { ColorName = "Orange", IsAvailable = true, ColorCode = "#FFA500", PriceAdjustment = 2m },
                new Color { ColorName = "Purple", IsAvailable = true, ColorCode = "#800080", PriceAdjustment = 3m },
                new Color { ColorName = "Blue", IsAvailable = true, ColorCode = "#1E90FF", PriceAdjustment = 2m },
                new Color { ColorName = "Fuchsia", IsAvailable = true, ColorCode = "#FF00FF", PriceAdjustment = 4m },

                // ⭐ NEW COLORS ⭐
                new Color { ColorName = "Creamy", IsAvailable = true, ColorCode = "#FFFDD0", PriceAdjustment = 1.5m },
                new Color { ColorName = "Bluish", IsAvailable = true, ColorCode = "#6CA0DC", PriceAdjustment = 2.5m },
                new Color { ColorName = "Yellowish", IsAvailable = true, ColorCode = "#FFF176", PriceAdjustment = 1m },
                new Color { ColorName = "Cherry", IsAvailable = true, ColorCode = "#D2042D", PriceAdjustment = 5m }
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
