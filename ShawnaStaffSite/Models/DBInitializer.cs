using System;
using System.Collections.Generic;
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
            if (context.User.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
           {
            new User{Name="Shawna"},
            new User{Name="Ivy"},
            new User{Name="Mikhail"}
           };
            foreach (User u in users)
            {
                context.User.Add(u);
            }
            context.SaveChanges();

            var forums = new ForumPosts[]
            {
            new ForumPosts{PostTopic="Lighting",PostText="The idea about using a lace curtain to defuse natural lighting is genius!",Name = new User {Name = "Shawna" },Date=DateTime.Parse("2020-11-10"), PostRating = 5},
            new ForumPosts{PostTopic="Reframing",PostText="I didn't understand reframing.",Name = new User {Name = "Ivy" },Date=DateTime.Parse("2020-7-16"), PostRating = 1},
            new ForumPosts{PostTopic="Golden Hour",PostText="This definitely gives me a reason to be up early.",Name = new User {Name = "Mikhail" },Date=DateTime.Parse("2021-1-3"), PostRating = 4}
            };
            foreach (ForumPosts f in forums)
            {
                context.ForumPosts.Add(f);
            }
            context.SaveChanges();
        }
    }
}
