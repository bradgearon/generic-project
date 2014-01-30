using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericProject.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GenericProject.Core.Data.EntityFramework.EntityFrameworkContext>
    {
        public Configuration()
        {
            // TODO: do actual migrations
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GenericProject.Core.Data.EntityFramework.EntityFrameworkContext context)
        {
            var seeder = new DatabaseSeeder(context);
            seeder.SeedDatabase();
        }
    }
}
