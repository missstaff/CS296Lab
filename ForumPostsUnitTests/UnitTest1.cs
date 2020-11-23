using Shawna_Staff.Controllers;
using Shawna_Staff.Models;
using Shawna_Staff.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using User = Shawna_Staff.Models.User;

namespace ForumPostsUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void AddBookTest()
        {
            // Arrange
            var repo = new FakeForumsRepository();
            var controller = new HomeController(repo);
            var post = new ForumPosts() 
            { 
              PostTopic = "Low lighting Techniques", 
              Name = new User() {Name = "Me" },
              PostText = "Really useful info!" 
            };
            // Act
            controller.Forum(post);
            // Assert
            var retrievePost = repo.Posts.ToList()[0];
            Assert.Equal(0, System.DateTime.Now.Date.CompareTo(retrievePost.Date.Date));
        }
    }
}
    
