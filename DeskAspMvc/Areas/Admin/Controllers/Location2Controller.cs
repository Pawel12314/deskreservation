using DeskAspMvc.Data;
using DeskAspMvc.services;
using DeskModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DeskAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                var status = _service.Create(location);
                this.ViewBag.Status = status;
                var lst = this._service.GetList();
                ViewBag.createid = location.id;
                return View("Index",lst);
                
                //return RedirectToAction("Index");
                //return View()
            }


            return View(location);
        } 


        public IActionResult Edit(int? id)
        {
            var status = this._service.DoesExist(id);
            if(status.hasSucceeded==false)
            {
                ViewBag.Status = status;
                var lst = this._context.locations.ToList();
                return View("Index",lst);
            }
            else
            {
                return View(this._service.GetById(id)??new Location());
            }
        }
        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                var status = _service.Edit(location,location.id);
                ViewBag.Status = status;
                ViewBag.editid = location.id;
                return View("Index",this._service.GetList());
            }
            return View(location);
        }

        public IActionResult Delete(int? id)
        {
            var status = _service.Delete(id);
            ViewBag.Status = status;
            return View("Index", this._service.GetList());
        }
    }
}
