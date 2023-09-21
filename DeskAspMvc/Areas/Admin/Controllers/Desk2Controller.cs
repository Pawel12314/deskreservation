using Microsoft.AspNetCore.Mvc;

namespace DeskAspMvc.Areas.Admin.Controllers
{
    public class Desk2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
