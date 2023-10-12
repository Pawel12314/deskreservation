using DeskAspMvc.Models.dto;
using DeskAspMvc.Models.Models;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using DeskAspMvc.services.Services2;
using DeskAspMvc.services.Services2.Methods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DeskAspMvc.Areas.Employee.Controllers
{

    [Area("Employee")]
    public class Reservation2Controller : Controller
    {
        private DeskService _deskService { get; set; }
        private ReservationServiceAdapter _reservationServiceAdapter { get; set; }
        private ReservationService _reservationService { get; set; }
        public Reservation2Controller(ReservationService reservationService,DeskService deskService, ReservationServiceAdapter reservationServiceAdapter)
        {
            this._deskService = deskService;
            this._reservationServiceAdapter = reservationServiceAdapter;
            this._reservationService = reservationService;
        }
        
        private SelectList GetDesksSelectList()
        {
            var selectlst = new SelectList(_deskService.GetList(), "id", "name");
            return selectlst;
        }
        private List<Desk> GetDesks()
        {
            var desks = this._deskService.GetList();
            foreach(var desk in desks)
            {
                if(desk.Location!=null)
                    desk.Location.Desks = null;
                else
                {
                    desk.Location = new Location()
                    {
                        Id = -1,
                        Name="desk has no assigned location"
                    };
                }
            }
            return desks;
        }
        public IActionResult Create()
        {
            ViewBag.Desks = this.GetDesks();
            return View();
        }
        [HttpGet]
        public ViewResult Index()
        {
            string ownerid = User.Identity.Name;
            var reservationlist = this._reservationService.GetList().Where(x=>x.OwnerId==ownerid);
            return View(reservationlist);   
        }

        [HttpGet]
        public PartialViewResult GetCalendarCreate(int deskid, int day, int month, int year,int weeks)
        {
            var status = this._reservationServiceAdapter.IsDeskAvailable(deskid);
    
            if(status.hasSucceeded == false)
            {
                ViewBag.calendarerror = status;
                MyCalendarDTO emptycalendar = this._reservationServiceAdapter.GetEmptyCalendar("desk is not available");
                return PartialView("CreateCalendarPartial", emptycalendar);
            }
            Desk? desk = (Desk?)this._deskService.GetById(deskid);
            if(desk.Location==null)
            {
                var deskstatus = ServiceOperationStatusObject
                    .GetOperationStatusObject(
                        new CreateOperationMessage(),
                        new DeskHasNoAssignedLocationMessage()
                    );
                MyCalendarDTO emptycalendar = this._reservationServiceAdapter.GetEmptyCalendar("desk has no assigned location");
                ViewBag.calendarerror = deskstatus;
                return PartialView("CreateCalendarPartial", emptycalendar);
            }
            var calendar =  this._reservationServiceAdapter.GetCalendar(day,month,year,weeks,desk);
            return PartialView("CreateCalendarPartial",calendar);
        }
        
        [HttpPost]
        public JsonResult PostReservation(String begindate, String enddate,int deskid)
        {
            Desk? desk = (Desk?)this._deskService.GetById(deskid);
            if(desk==null)
            {
                var deskstatus = ServiceOperationStatusObject
                    .GetOperationStatusObject(
                        new CreateOperationMessage(),
                        new NotFoundMessage()
                    );
                return Json(deskstatus);
            }
            Reservation reservation = new Reservation();
            String userid = User.Identity.Name;
            ServiceOperationStatusObject status
                = this._reservationServiceAdapter
                .PersistReservation(begindate, enddate, userid, 
                new CreateReservationMethods(), 
                reservation, desk);
            return Json(status);
        }
        [HttpPost]
        public JsonResult UpdateReservation(String begindate, String enddate, int reservationid)
        {
            
            Reservation? reservation = (Reservation?) this._reservationService.GetById(reservationid);

            if (reservation == null)
            {
                var reservationstatus = ServiceOperationStatusObject
                    .GetOperationStatusObject(
                        new CreateOperationMessage(),
                        new NotFoundMessage()
                    );
                return Json(reservationstatus);
            }
            Desk desk = reservation.Desk;
            String userid = User.Identity.Name;
            ServiceOperationStatusObject status
                = this._reservationServiceAdapter
                .PersistReservation(begindate, enddate, userid,
                new UpdateReservationMethods(),
                reservation, desk); ;

            return Json(status);
        }

        [HttpGet]
        public ViewResult GetEditPage(int? reservationid)
        {
            var status = this._reservationService.DoesExist(reservationid);
            if (status.hasSucceeded == false)
            {
                string ownerid = User.Identity.Name;
                var reservationlist = this._reservationService.GetList().Where(x => x.OwnerId == ownerid);
                ViewBag.Status = status;
                return View("Index",reservationlist); 
            }
            Reservation res = (Reservation)this._reservationService.GetById(reservationid);
            ViewBag.ReservationId = reservationid;
            ViewBag.BeginDate = res.BeginDate;
            ViewBag.EndDate = res.EndDate;
            
            return View("Edit");
        }
        [HttpGet]
        public PartialViewResult GetCalendarEdit(int reservationid, int day, int month, int year, int weeks)
        {
            var status = this._reservationService.DoesExist(reservationid);
            if (status.hasSucceeded == false)
            {
                ViewBag.calendarerror = status;
                var emptycalendar = this._reservationServiceAdapter.GetEmptyCalendarForEdit("reservation was not found");
                return PartialView("EditCalendarPartial", emptycalendar);
            }
            Reservation reservation = (Reservation)this._reservationService.GetById(reservationid);
            int deskid = reservation.DeskId;
            status = this._reservationServiceAdapter.IsDeskAvailable(deskid);
            if (status.hasSucceeded == false)
            {
                ViewBag.calendarerror = status;
                MyCalendarDTO emptycalendar = this._reservationServiceAdapter.GetEmptyCalendarForEdit("desk is unavailable");
                return PartialView("EditCalendarPartial", emptycalendar);
            }
            var calendar = this._reservationServiceAdapter.GetExistingCalendar(reservationid,day,month,year,weeks);
            return PartialView("EditCalendarPartial", calendar);
        }
        public ViewResult Delete(int? id)
        {
            var status = this._reservationService.Delete(id);       
            ViewBag.Status = status;
            string ownerid = User.Identity.Name;
            var reservationlist = this._reservationService.GetList().Where(x => x.OwnerId == ownerid);
            return View("Index",reservationlist);
            
            
        }
    }
}
