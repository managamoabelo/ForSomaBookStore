using ForSomaBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ForSomaBookStore.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var profile = new Profile
            {
                FullName = user.UserName,
                Email = user.Email,
                Institution = "Not set",
                StudentNumber = "Not set",
                Bio = "No bio yet"
            };

            return View(profile);
        }
    }
}