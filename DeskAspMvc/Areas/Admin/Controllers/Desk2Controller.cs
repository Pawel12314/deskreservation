using DeskAspMvc.Data;
using DeskAspMvc.services.Services2;
using DeskModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeskAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Desk2Controller : Controller
    {

        private ApplicationDbContext _context { get; set; }
        private DeskService2 _service { get; set; }
        public Desk2Controller(ApplicationDbContext context, DeskService2 service)
        {
            this._context = context;
            this._service = service;
        }
        public IActionResult Index()
        {
            var lst = this._service.GetList();
            return View(lst);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Desk entry)
        {
            if (ModelState.IsValid)
            {
                var status = _service.Create(entry);
                this.ViewBag.Status = status;
                var lst = this._service.GetList();
                ViewBag.createid = entry.id;
                return View("Index", lst);

                //return RedirectToAction("Index");
                //return View()
            }


            return View(entry);
        }


        public IActionResult Edit(int? id)
        {
            var status = this._service.DoesExist(id);
            if (status.hasSucceeded == false)
            {
                ViewBag.Status = status;
                var lst = this._service.GetList();
                return View("Index", lst);
            }
            else
            {
                return View(this._service.GetById(id) ?? new Desk());
            }
        }
        [HttpPost]
        public IActionResult Edit(Desk entry)
        {
            if (ModelState.IsValid)
            {
                var status = _service.Edit(entry, entry.id);
                ViewBag.Status = status;
                ViewBag.editid = entry.id;
                return View("Index", this._service.GetList());
            }
            return View(entry);
        }

        public IActionResult Delete(int? id)
        {
            var status = _service.Delete(id);
            ViewBag.Status = status;
            return View("Index", this._service.GetList());
        }
    }
}
