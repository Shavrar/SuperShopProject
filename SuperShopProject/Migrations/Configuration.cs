using SuperShopProject.Constants;
using SuperShopProject.Models;

namespace SuperShopProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SuperShopProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SuperShopProject.Models.ApplicationDbContext context)
        {
            var adminRole = new Role { Id = Guid.NewGuid(), Name = Consts.AdministratorRole };
            UpdateRole(context, adminRole);
            var customerRole = new Role { Id = Guid.NewGuid(), Name = Consts.CustomerRole };
            UpdateRole(context, customerRole);
            var adminId = new Guid("df2d7d63-623e-435c-a3f9-24c13ba26b43");
            context.Users.AddOrUpdate(new User
            {
                Id = adminId,
                Email = "admin@shop.com",
                UserName = "admin@shop.com",
                LastName = "Thrive",
                FirstName = "Admin",
                Type = UserType.Administrator,
                PasswordHash = "AJjlTE+gGxfqxfQAKAywR9Eyg5tzU4LCoFmMQ46TL5LIrXu5l8s1QwxAAg+DFd8J1g==", //admin_HPX1
                SecurityStamp = "96d36c8e-87b6-481b-90eb-3c4d668e0e4d",
                Roles = { new UserRole { UserId = adminId, RoleId = adminRole.Id } }
            });
        }

        private void UpdateRole(ApplicationDbContext context, Role role)
        {
            context.Roles.AddOrUpdate(r => r.Name, role);           
        }
    }


}
