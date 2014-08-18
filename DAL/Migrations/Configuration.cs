using DAL.Models;

namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;
    internal sealed class Configuration : DbMigrationsConfiguration<UnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UnitOfWork context)
        {
                context.Roles.AddOrUpdate(
                  new Role { Name = "User" },
                  new Role { Name = "Moderator" },
                  new Role { Name = "Admin" }
                );
            
        }
    }
}
