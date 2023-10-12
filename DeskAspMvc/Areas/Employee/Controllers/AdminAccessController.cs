using DeskAspMvc.Data;
using DeskAspMvc.services.AuthorizeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeskAspMvc.Areas.Employee.Controllers
{

    [Area("Employee")]
    public class AdminAccessController : Controller
    {
        private MyAuthorizeService _myAuthorizationService { get; set; }
        public AdminAccessController(
            MyAuthorizeService myAuthorizeService
            )
        {
            this._myAuthorizationService = myAuthorizeService;
        }

        public async Task<IActionResult> Revoke()
        {
            string username = User.Identity.Name;
            await this._myAuthorizationService.RemoveFromRole(username);
            return View();
        }
        public async Task<IActionResult> Gain()
        {
            string username = User.Identity.Name;
            await this._myAuthorizationService.AddToAdminRole(username);
            return View();
        }

        [Authorize(Policy = "Administrator")]
        public IActionResult AdminTask()
        {
            return View();
        }
    }
}

