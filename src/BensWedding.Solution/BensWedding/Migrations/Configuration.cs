namespace BensWedding.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Models.ApplicationDbContext context)
        {
            context.AttendingWhen.AddOrUpdate(
                a => a.Description,
                new Attending { Description = "Day" },
                new Attending { Description = "Evening" }
            );            
        
            context.MenuOptions.AddOrUpdate(
                m => m.Title,
                new MenuOption { Title = "Option A", Description = "Roast beef, yorkshire pudding, roast potatoes served with vegetables" },
                new MenuOption { Title = "Option B", Description = "Breast of chicken stuffed with sundried tomatoes, pesto & mozzarella with honey & thyme sauce served with vegetables" }
            );

            context.PartyMembers.AddOrUpdate(
                p => p.Name,
                new PartyMember { Title = "Chief Bridesmaid", Name = "Megan Jones", Description = "Hayley’s cousin", ImageUrl = "~/Content/images/megan.jpeg", Order = 0 },
                new PartyMember { Title = "Junior Bridesmaid", Name = "Aimee Jones", Description = "Ben's sister", ImageUrl = "~/Content/images/aimee.jpeg", Order = 1 },
                new PartyMember { Title = "Bridesmaid", Name = "Amy Price", Description = "Hayley’s school friend since 11", ImageUrl = "~/Content/images/amy.jpeg", Order = 2 },
                new PartyMember { Title = "Bridesmaid", Name = "Liv O'Toole", Description = "Hayley’s school friend since 11", ImageUrl = "~/Content/images/liv.jpeg", Order = 3 },
                new PartyMember { Title = "Bridesmaid", Name = "Sarah Wicks", Description = "Ben's sister", ImageUrl = "~/Content/images/sarah.jpg", Order = 4 },
                new PartyMember { Title = "Bridesmaid", Name = "Samantha Sargeant", Description = "Ben's sister", ImageUrl = "~/Content/images/sam.jpeg", Order = 5 },
                new PartyMember { Title = "Best Man", Name = "Matthew Jones", Description = "Ben's friend", ImageUrl = "~/Content/images/matt.jpeg", Order = 6 },
                new PartyMember { Title = "Groomsman", Name = "Tom Cowdale", Description = "Ben's cousin", ImageUrl = "~/Content/images/tom.jpeg", Order = 7 },
                new PartyMember { Title = "Groomsman", Name = "Russ Wicks", Description = "Ben's brother in law", ImageUrl = "~/Content/images/russ.jpg", Order = 9 },
                new PartyMember { Title = "Groomsman", Name = "Gareth Barker", Description = "Hayley's brother", ImageUrl = "~/Content/images/gareth.jpeg", Order = 8 }
            );

            CreateDefaultUsers();
        }

        private void CreateDefaultUsers()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!roleManager.RoleExists("User"))
            {
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            CreateUser(manager, "russwicks1@hotmail.com", "secure@Pass!1", "Russ", "Wicks", "russwicks1@hotmail.com", new[] { "User", "Admin" });
            CreateUser(manager, "ben.sargeant@outlook.com", "secure@Pass!1", "Ben", "Sargeant", "ben.sargeant@outlook.com", new[] { "User", "Admin" });

        }

        private void CreateUser(UserManager<ApplicationUser> manager,
            string userName, string password, string firstName, string lastName, string email, string[] roles)
        {
            var adminUser = manager.FindByName(userName);

            if (adminUser == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true,
                    DisplayName = $"{firstName} {lastName}"
                };

                manager.Create(user, password);

                adminUser = manager.FindByName(userName);

                manager.AddToRoles(adminUser.Id, roles);
            }
        }
    }
}
