using DeskAspMvc.Data;
using Microsoft.AspNetCore.Identity;

namespace DeskAspMvc.services.AuthorizeServices
{
    public class MyAuthorizeService
    {
        private UserManager<IdentityUser> _userManager { get; set; }
        private RoleManager<IdentityRole> _roleManager { get; set; }
        private ApplicationDbContext _context { get; set; }
        public MyAuthorizeService(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            ApplicationDbContext context
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }
        
        private async Task CreateAdminRole()
        {
            IdentityResult roleResult;
            var roleCheck = await _roleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
        public async Task RemoveFromRole(string username)
        {
            await CreateAdminRole();
            IdentityUser user = GetUser(username).Result;
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }
        }

        public async Task AddToAdminRole(string username)
        {
            await CreateAdminRole();
            IdentityUser user = GetUser(username).Result;
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
        private async Task<IdentityUser> GetUser(string name)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(name);
            return user;
        }

    }
}
