using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Models
{
    public class DBInitializer
    {
        public static void Initializer(ForumContext context)
        {
            context.Database.EnsureCreated();

            //Look for any Users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

           // var result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;

            var users = new AppUser[]
           {
            new AppUser{Name="Shawna"},
            new AppUser{Name="Ivy"},
            new AppUser{Name="Mikhail"}
           };
            foreach (AppUser u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var forums = new ForumPosts[]
            {
            new ForumPosts{PostTopic="Lighting",PostText="The idea about using a lace curtain to defuse natural lighting is genius!",Name = new AppUser {Name = "Shawna" },Date=DateTime.Parse("2020-11-10"), PostRating = 5},
            new ForumPosts{PostTopic="Reframing",PostText="I didn't understand reframing.",Name = new AppUser {Name = "Ivy" },Date=DateTime.Parse("2020-7-16"), PostRating = 1},
            new ForumPosts{PostTopic="Golden Hour",PostText="This definitely gives me a reason to be up early.",Name = new AppUser {Name = "Mikhail" },Date=DateTime.Parse("2021-1-3"), PostRating = 4}
            };
            foreach (ForumPosts f in forums)
            {
                context.ForumPosts.Add(f);
            }
            context.SaveChanges();
        }

       public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<AppUser> userManager =
                serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "Sesame1!";
            string roleName = "Admin";

            //if role doesn't exist create it
            if(await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            //if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                AppUser user = new AppUser { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }


        public static async Task CreateMemberUser(IServiceProvider serviceProvider)
        {
            UserManager<AppUser> userManager =
                serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "";
            string password = "";
            string roleName = "Member";

            //if role doesn't exist create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            //if username doesn't exist, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                AppUser user = new AppUser { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
