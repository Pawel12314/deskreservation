using DeskAspMvc.Data;
using DeskAspMvc.Models.dto;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.Services2;
using DeskAspMvc.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DeskAspMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Administrator")]
    public class Desk2Controller : Controller
    {
        private DeskService _deskService { get; set; }
        private LocationService _locationService { get; set; }
        public Desk2Controller(DeskService deskService, LocationService locationService)
        {
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
            var selectlst = new SelectList(_locationService.GetList(), "Id", "Name");

            SelectListItem selListItem = new SelectListItem() { Value = "", Text = "no location" };
            List<SelectListItem> newList = new List<SelectListItem>(selectlst);
            newList.Insert(0,selListItem);
            return new SelectList(newList, "Value", "Text", null); ;
        }
        public IActionResult Create()
        {     
            ViewBag.locationKey = getLocations();
            return View();
        }
        [HttpPost]
        
        public IActionResult Create(Desk entry)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.locationKey = getLocations();
                return View(entry);
            }
            var status = _deskService.Create(entry);
            this.ViewBag.Status = status;
            var lst = this._deskService.GetListWithLocation();
            ViewBag.createid = entry.Id;
            return View("Index", lst);
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
            ViewBag.locationKey = getLocations();
            return View(this._deskService.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(Desk entry)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.locationKey = getLocations();
                return View(entry);
            }
            var status = _deskService.Edit(entry, entry.Id);
            ViewBag.Status = status;
            ViewBag.editid = entry.Id;
            return View("Index", this._deskService.GetListWithLocation());
        }

        public IActionResult Delete(int? id)
        {
            var status = _deskService.Delete(id);
            ViewBag.Status = status;
            return View("Index", this._deskService.GetList());
        }        
    }
}
