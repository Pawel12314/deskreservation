using DeskAspMvc.Data;
using DeskAspMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DeskAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy ="Administrator")]
    public class LocationController : Controller
    {

        private ApplicationDbContext _context { get; set; }
        
        public LocationController(ApplicationDbContext con)
        {
            this._context = con;
        }
        
        public IActionResult Index()
        {
            var list = _context.locations.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            //  return _context.AdCategory.Count();

            
            //Console.Out.WriteLine(_context.AdCategory.Count());
            //System.Diagnostics.Debug.WriteLine(_context.AdCategory.Count());
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(location);
        }
        public IActionResult Edit(int? id)
        {
            
            bool doesExist = _context.locations.
                Where(x => x.id == id).Any();
            if(doesExist==false)
            {
                return RedirectToAction("Index");
            }
            var model = _context.locations.
                Where(x => x.id == id)
                .Include(x=>x.desks)
                .SingleOrDefault();

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Update(location);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }
        
        public IActionResult Delete(int? id)
        {
            bool status = _context.locations.Where(x => x.id == id).Any();
            
            if(status==false)
            {
                return RedirectToAction("Index");
            }
            bool relateddesksempty = _context.locations
                .Where(x => x.id == id)
                .Include(x => x.desks)
                .SingleOrDefault()
                .desks.IsNullOrEmpty();
            if(relateddesksempty==false)
            {
                return RedirectToAction("Index");
            }
            var model = _context
                .locations
                .Where(x => x.id == id)
                .SingleOrDefault();
            _context.locations.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public string AjaxTest()
        {

            return "hello from ajax";
        }
       
    }
}
