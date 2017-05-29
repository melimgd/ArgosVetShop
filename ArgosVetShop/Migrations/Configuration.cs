namespace ArgosVetShop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ArgosVetShop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ArgosVetShop.Models.ApplicationDbContext context)
        {
            
            if (!context.Services.Any())
            {
                context.Services.AddOrUpdate(
                        new ServiceModel { ServiceName = "Veterinary consultation", Price = 50.00M,  ServiceDescription = "General revision of your Pet" },
                        new ServiceModel { ServiceName = "Haircut and Bath", Price = 230.00M, ServiceDescription = "An awesome Haircut and a refreshing Bath for your Pet" },
                        new ServiceModel { ServiceName = "Haircut", Price = 150.25M, ServiceDescription = "An awesome Haircut for your Pet" },
                        new ServiceModel { ServiceName = "Bath", Price = 100.25M, ServiceDescription = "A refreshing Bath for your Pet" },
                        new ServiceModel { ServiceName = "Flea Bath", Price = 150.50M, ServiceDescription = "Let's remove those annoying fleas and tick from your Pet" }
                    );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                string[] roles = { "Veterinarian", "User" };
                if (!context.Roles.Any())
                {
                    foreach (var role in roles)
                    {
                        var roleToCreate = new IdentityRole(role);
                        roleManager.Create(roleToCreate);
                    }
                }
                var vet = userManager.FindByName("melissa.mgd");
                if (vet == null)
                {
                    var newVet = new ApplicationUser()
                    {
                        FullName = "Melissa Guardado",
                        UserName = "melissa.mgd",
                        Email = "melissarules@gmail.com",
                        PhoneNumber = "6141255736",
                        IsEnabled = true
                    };

                    userManager.Create(newVet, "PeeweeAndBob1001945!");
                    userManager.SetLockoutEnabled(newVet.Id, false);
                    userManager.AddToRole(newVet.Id, "Veterinarian");
                   
                }
            }

        }
    }
}
