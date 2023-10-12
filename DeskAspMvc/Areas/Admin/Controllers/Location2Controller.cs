using DeskAspMvc.Data;
using DeskAspMvc.services;
using DeskAspMvc.services.Services2;
using DeskAspMvc.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace DeskAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Administrator")]
    public class Location2Controller : Controller
    {

        private ApplicationDbContext _context { get; set; }
        private LocationService _service { get; set; }
        public Location2Controller(ApplicationDbContext context,LocationService service)
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
        public IActionResult Create(Location entry)
        {
            if(!ModelState.IsValid)
            {
                return View(entry);
            }
            var status = _service.Create(entry);
            this.ViewBag.Status = status;
            var lst = this._service.GetList();
            ViewBag.createid = entry.Id;
            return View("Index", lst);
        }


        public IActionResult Edit(int? id)
        {
            var status = this._service.DoesExist(id);
            if(status.hasSucceeded==false)
            {
                ViewBag.Status = status;
                var lst = this._service.GetList();
                return View("Index",lst);
            }
            return View(this._service.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(Location entry)
        {
            if(!ModelState.IsValid)
            {
                return View(entry);
            }
            var status = _service.Edit(entry, entry.Id);
            ViewBag.Status = status;
            ViewBag.editid = entry.Id;
            return View("Index", this._service.GetList());
        }

        public IActionResult Delete(int? id)
        {
            var status = _service.Delete(id);
            ViewBag.Status = status;
            return View("Index", this._service.GetList());
        }
    }
}
