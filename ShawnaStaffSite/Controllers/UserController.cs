using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shawna_Staff.Models;
using Shawna_Staff.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shawna_Staff.Controllers
{
    [Authorize(Roles = "Admin")]
    //[Area("Admin")]
    public class UserController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IForums  repo;

        public UserController(UserManager<AppUser> usrmangr,
                RoleManager<IdentityRole> rolemangr,
                IForums r)
        {
            userManager = usrmangr;
            roleManager = rolemangr;
            repo = r;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> users = new List<AppUser>();
            foreach(AppUser user in userManager.Users)
            {
                user.RolesNames = await userManager.GetRolesAsync(user);
                users.Add(user);
            }
            UserVM model = new UserVM
            {
                Users = users,
                Roles = (IEnumerable<IdentityRole>)roleManager.Roles
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityResult result = null;
            AppUser user = await userManager.FindByIdAsync(id);
            if(user != null)
            {
                if (0 == (from r in repo.Posts
                          where r.Name.Name == user.Name
                          select r).Count<ForumPosts>())
                {
                    result = await userManager.DeleteAsync(user);
                }
                else
                {
                    result = IdentityResult.Failed(new IdentityError()
                    { Description = "User's reviews must be deleted first" });
                }
                if (!result.Succeeded)
                {
                    string errorMessage = "";
                    foreach(IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");
            if(adminRole == null)
            {
                TempData["message"] = "Admin does not exist!"
                    + "Click 'Create Admin Role' button to create it";
            }
            else
            {
                AppUser user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole(string id)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            return RedirectToAction("Index");
        }

    }
}
