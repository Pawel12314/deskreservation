using DeskAspMvc.Models.Models;
using DeskAspMvc.services.Services2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeskAspMvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize("Administrator")]
    public class ReservationController : Controller
    {

        private ReservationService _reservationService { get; set; }
        public ReservationController(ReservationService reservationService)
        {
            this._reservationService = reservationService;
        }
        public IActionResult Index()
        {
            var list = this._reservationService.GetList();
            return View(list);
        }
        public IActionResult Delete(int? id)
        {
            var status = this._reservationService.Delete(id);
            var list = this._reservationService.GetList();
            ViewBag.Status = status;
            return View("Index", list);

        }
    }
}
