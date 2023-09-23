using DeskAspMvc.Data;
using DeskAspMvc.Models.dto;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.Services2;
using DeskModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace DeskAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Desk2Controller : Controller
    {

        private ApplicationDbContext _context { get; set; }
        private DeskService2 _deskService { get; set; }
        private LocationService2 _locationService { get; set; }
        public Desk2Controller(ApplicationDbContext context, DeskService2 deskService, LocationService2 locationService)
        {
            this._context = context;
            this._deskService = deskService;
            this._locationService = locationService;
        }
        public IActionResult Index()
        {
            var lst = this._deskService.GetListWithLocation();
            return View(lst);
        }
        private SelectList getLocations()
        {
            var selectlst = new SelectList(_locationService.GetList(), "id", "name");

            SelectListItem selListItem = new SelectListItem() { Value = "", Text = "no location" };

            //Create a list of select list items - this will be returned as your select list
            List<SelectListItem> newList = new List<SelectListItem>(selectlst);

            //Add select list item to list of selectlistitems
            newList.Insert(0,selListItem);

            //Return the list of selectlistitems as a selectlist
            //return new SelectList(newList, "Value", "Text", null);
            return new SelectList(newList, "Value", "Text", null); ;
        }
        public IActionResult Create()
        {
            
            ViewBag.locationKey = getLocations();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Desk entry)
        {
            if (ModelState.IsValid)
            {
                var status = _deskService.Create(entry);
                this.ViewBag.Status = status;
                var lst = this._deskService.GetListWithLocation();
                ViewBag.createid = entry.id;
                return View("Index", lst);
            }
            ViewBag.locationKey = getLocations();
            return View(entry);
        }

        public IActionResult Edit(int? id)
        {
            var status = this._deskService.DoesExist(id);
            if (status.hasSucceeded == false)
            {
                ViewBag.Status = status;
                var lst = this._deskService.GetList();
                return View("Index", lst);
            }
            else
            {
                ViewBag.locationKey = getLocations();
                return View(this._deskService.GetById(id) ?? new Desk());
            }
        }
        [HttpPost]
        public IActionResult Edit(Desk entry)
        {
            if (ModelState.IsValid)
            {
                var status = _deskService.Edit(entry, entry.id);
                ViewBag.Status = status;
                ViewBag.editid = entry.id;
                return View("Index", this._deskService.GetListWithLocation());
            }
            ViewBag.locationKey = getLocations();
            return View(entry);
        }

        public IActionResult Delete(int? id)
        {
            var status = _deskService.Delete(id);
            ViewBag.Status = status;
            return View("Index", this._deskService.GetList());
        }
        [HttpGet]
        public JsonResult AjaxGetDesks()
        {
            var lst = this._locationService.GetList();
            return Json(lst);
        }
        [HttpPost]
        public JsonResult AjaxPostLocation([FromBody] DeskDTO deskDTO)
        {
            var doesExistLocation = this._locationService.DoesExist(deskDTO.locationid);
            var doesExistDesk = this._deskService.DoesExist(deskDTO.deskid);
            if(doesExistLocation.hasSucceeded==false)
            {
                return Json(doesExistLocation);
            }
            if (doesExistDesk.hasSucceeded == false)
            {
                return Json(doesExistDesk);
            }
            var desk = (Desk)this._deskService.GetById(deskDTO.deskid);
            var location = (Location)this._locationService.GetById(deskDTO.locationid);

            var status = this._deskService.SetDeskLocation(desk, location);
            return Json(status);
        }
        [HttpPost]
        public JsonResult AjaxDeleteLocation([FromBody] DeskDTO deskDTO)
        {
            var doesExistDesk = this._deskService.DoesExist(deskDTO.deskid);
            if (doesExistDesk.hasSucceeded == false)
            {
                return Json(doesExistDesk);
            }
            var desk = (Desk)this._deskService.GetById(deskDTO.deskid);
            
            var status = this._deskService.RemoveDeskLocation(desk);
            return Json(status);
        }
        [HttpGet]
        public string AjaxLoadRelatedLocation(int? id)
        {
            var doesExistLocation = this._locationService.DoesExist(id);
            var doesExistDesk = this._deskService.DoesExist(id);
            if (doesExistLocation.hasSucceeded == false)
            {
                return JsonConvert.SerializeObject(
                    doesExistLocation, 
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
            }
            if (doesExistDesk.hasSucceeded == false)
            {
                return JsonConvert.SerializeObject(
                    doesExistDesk, 
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
            }
            var desk = (Desk)this._deskService.GetById(id);
            //desk.location.desks = null;
            return JsonConvert.SerializeObject(
                desk.location, 
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            
            //Json(desk.location);

            //return Json(desk.location);
        }
    }
}
