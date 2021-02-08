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
            
            AppUser shawnaStaff = new AppUser
            {
                UserName = "SStaff",
                Name = "Shawna Staff"
            };
            context.Users.Add(shawnaStaff);
            context.SaveChanges();

            AppUser ivyStaff = new AppUser
            {
                UserName = "IStaff",
                Name = "Ivy Staff"
            };
            context.Users.Add(ivyStaff);
            context.SaveChanges();

            AppUser mikhailGuidesse = new AppUser
            {
                UserName = "MGuidesse",
                Name = "Mikhail Guidesse"
            };
            context.Users.Add(mikhailGuidesse);
            context.SaveChanges();

            ForumPosts Lighting = new ForumPosts
            {
                PostTopic = "Lighting",
                PostText = "The idea about using a lace curtain to defuse natural lighting is genius!",
                Name = shawnaStaff,
                Date = DateTime.Parse("2020-11-10"),
                PostRating = 5,
             };
            context.ForumPosts.Add(Lighting);
            context.SaveChanges();

            ForumPosts Reframing = new ForumPosts
            {
                PostTopic = "Reframing",
                PostText = "I didn't understand reframing.",
                Name = ivyStaff,
                Date = DateTime.Parse("2020-7-16"),
                PostRating = 1
            };
            context.ForumPosts.Add(Reframing);
            context.SaveChanges();

            ForumPosts GoldenHour = new ForumPosts
            {
                PostTopic = "Golden Hour",
                PostText = "This definitely gives me a reason to be up early.",
                Name = mikhailGuidesse,
                Date = DateTime.Parse("2021-1-3"),
                PostRating = 4
            };
            context.ForumPosts.Add(GoldenHour);
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


        public static async Task CreateMemberUser(IServiceProvider serviceProvider)
        {
            UserManager<AppUser> userManager =
                serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "Miss";
            string password = "Abc123!";
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