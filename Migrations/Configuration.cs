namespace Bloomfiy_final.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Bloomfiy_final.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Bloomfiy_final.Models.ApplicationDbContext";
        }

        protected override void Seed(Bloomfiy_final.Models.ApplicationDbContext context)
        {
            
        }
    }
}
