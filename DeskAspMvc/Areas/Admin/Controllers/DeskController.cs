using DeskAspMvc.Data;
using DeskAspMvc.Models;
using DeskAspMvc.Models.dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy ="Administrator")]
    public class DeskController : Controller
    {
        
        private ApplicationDbContext _context { get; set; }

        public DeskController(ApplicationDbContext con)
        {
            this._context = con;
        }

        public IActionResult Index()
        {
            var list = _context.desks.Include(x=>x.location).Include(x=>x.reservation).ToList();
            List<AdminDeskDTO> admindesks = new List<AdminDeskDTO>();
            foreach(var desk in list)
            {
                AdminDeskDTO dtodesk = new AdminDeskDTO();
                dtodesk.id = desk.id;
                dtodesk.LocationName = desk.location ==null ? "no location provided":desk.location.name;
                dtodesk.Name = desk.name;
                dtodesk.isAvailable = desk.available;
                if(desk.reservation!=null)
                {
                    string? name = _context.Users.Where(x => x.Id.Equals(desk.reservation.ownerId)).SingleOrDefault().Email;
                    dtodesk.reservedForUsername = name == null ? "---" : name;
                }
                admindesks.Add(dtodesk);
            }
            return View(admindesks);
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
        public IActionResult Create(Desk desk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(desk);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(desk);
        }
        public IActionResult Edit(int? id)
        {
            
            bool doesExist = _context.desks.
                Where(x => x.id == id).Any();
            if (doesExist == false)
            {
                return RedirectToAction("Index");
            }
            var model = _context.desks.
                Where(x => x.id == id)
                .Include(x=>x.location)
                
                .SingleOrDefault();

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Desk desk)
        {
            if (ModelState.IsValid)
            {
                
                //var newdesk = _context.desks.Where(x => x.id == desk.id).SingleOrDefault();
                //_context.Entry(newdesk).State = EntityState.Modified;
                //newdesk.id = desk.id;
                //newdesk.name = desk.name;
                //newdesk.locationKey=desk.locationKey
                // newdesk.
                //_context.Entry(desk).State = EntityState.Modified;
                //_context.Entry(desk).Reference(c => c.location).IsModified = false;
                //_context.Entry(desk).Property(c => c.).IsModified = false;

                _context.Update(desk);
               

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(desk);
        }
        [HttpPost]
        public IActionResult AjaxUpdate([FromBody]DeskDTO deskdto)
        {
            bool deskexist = _context.desks.Where(x => x.id == deskdto.deskid).Any();
            bool locationexist = _context.locations.Where(x => x.id == deskdto.locationid).Any();
            if (!deskexist)
            {
                return RedirectToAction("Index");
            }
            var desk = _context.desks.Where(x => x.id == deskdto.deskid).SingleOrDefault();

            Location location = null;
            if (locationexist)
                location = _context.locations.Where(x => x.id == deskdto.locationid).SingleOrDefault();
            else
                desk.locationKey = null;
            desk.location = location;
            _context.desks.Update(desk);
            _context.SaveChanges();
            return Content("location should be added to desk");
        }
        public IActionResult Delete(int? id)
        {
            bool status = _context.desks.Where(x => x.id == id).Any();
            if (status == false)
            {
                return RedirectToAction("Index");
            }
            var model = _context.desks.Where(x => x.id == id).Include(x=>x.reservation).SingleOrDefault();
            if (model.reservation!=null)
            {
                return RedirectToAction("Index");
            }

            _context.desks.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public string AjaxTest()
        {

            return "hello from ajax";
        }
        [HttpGet]
        public JsonResult AjaxGetLocations()
        {
            var list = _context.locations.ToList();
            return Json(list);
        }
    }
}
