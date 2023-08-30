using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestController : Controller
    {
        [Authorize("Administrator")]
        public IActionResult Index()
        {
            return Content("you are admin");
        }

        public IActionResult Index2()
        {
            return Content("you are not an admin");
        }
        public IActionResult Name()
        {
            string name = User.Identity.Name;
            return Content(name);
        }

    }
}
