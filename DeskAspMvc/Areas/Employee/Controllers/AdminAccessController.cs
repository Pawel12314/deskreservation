using DeskAspMvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeskAspMvc.Areas.Employee.Controllers
{

    [Area("Employee")]
    public class AdminAccessController : Controller
    {
        private UserManager<IdentityUser> _userManager { get; set; }
        private RoleManager<IdentityRole> _roleManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        public AdminAccessController(
            RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }

        public async Task<IActionResult> Revoke()
        {
            IdentityResult roleResult;
            var roleCheck = await _roleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //Create the roles and seed them to the database 
                roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //UserManager.GetEmailAsync()

            // Assign Admin role to newly registered user
            IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                //await _context.SaveChangesAsync();
            }

            return View();
        }
        public async Task<IActionResult> Gain()
        {
            IdentityResult roleResult;
            var roleCheck = await _roleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //Create the roles and seed them to the database 
                roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //UserManager.GetEmailAsync()

            // Assign Admin role to newly registered user
            IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                
                await _userManager.AddToRoleAsync(user, "Admin");
                //await _context.SaveChangesAsync();
            }

            return View();
        }

        [Authorize(Policy = "Administrator")]
        public IActionResult AdminTask()
        {
            return View();
        }
    }
}

