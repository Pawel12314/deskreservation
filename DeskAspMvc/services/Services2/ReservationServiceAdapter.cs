using DeskAspMvc.Models.dto;
using DeskAspMvc.Models.Models;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using DeskAspMvc.services.Services2.Methods;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using NuGet.Protocol.Plugins;
using System.Globalization;
using System.Reflection;

namespace DeskAspMvc.services.Services2
{
    public sealed class ReservationServiceAdapter
    {
        private MyDateService _dateService { get; set; }
        private ReservationService _reservationService { get; set; }
        private DeskService _deskService { get; set; }
        public ReservationServiceAdapter(MyDateService myDateService, 
            ReservationService reservationService,
            DeskService deskService)
        {
            this._dateService = myDateService;
            this._reservationService = reservationService;
            this._deskService = deskService;
        }

        private bool GetDateFromProps(int day,int month,int year, out DateTime dt)
        {
            DateTime dateobj = new DateTime();
            string datestr = day + "/" + month + "/" + year;
            bool status = DateTime.TryParse(datestr,out dt);
            return status;
            
        }
        
        
        private MyDate GetMyDateFromList(int day,int month,int year, List<MyDate> mydates)
        {
            DateTime localdate = new DateTime();
            this.GetDateFromProps(day, month, year, out localdate);
            
            MyDate localmydate = new MyDate();
            localmydate.Date = localdate;
            localmydate.Reservations = new List<Reservation>();
            
            MyDate date = 
                mydates
                .Where(
                x => x.Date.Day == day
                ).Where(
                x => x.Date.Month == month
                ).Where(
                x => x.Date.Year == year
                )
                .FirstOrDefault() ?? localmydate;
            return date;
        }
        public ServiceOperationStatusObject IsDeskAvailable(int deskid)
        {
            Desk? nullabledesk = (Desk?)this._deskService.GetById(deskid);
            if(nullabledesk==null)
            {
                return ServiceOperationStatusObject
                    .GetOperationStatusObject(
                        new GetOperationMessage(),
                        new NotFoundMessage()
                    );
            }
            else
            {
                Desk desk=nullabledesk;
                if(desk.Available==false)
                {
                    return ServiceOperationStatusObject
                        .GetOperationStatusObject(
                            new GetOperationMessage(),
                            new DeskIsNotAvailableMessage()
                        );
                }
            }
            return ServiceOperationStatusObject
                .GetOperationStatusObject(
                    new GetOperationMessage(),
                    new SucceededMessage()
                );

        }
        public List<MyDateDTO> GetMyDatesList(int beginDay,int beginMonth,int beginYear,int weeks,int deskid,int reservationid,IPersistReservationHelperMethods helpermethods)
        {
            DateTime begindate;
            this.GetDateFromProps(beginDay, beginMonth, beginYear, out begindate);
            var mydateslist = this._dateService.GetList();
            List<MyDateDTO> calendar = new List<MyDateDTO>();
            int weekadjustmenent = 1 - (int)begindate.DayOfWeek;
            
            for (int i = weekadjustmenent; i < 7 * weeks; ++i)
            {
                var nextdaydate = begindate.AddDays(i);
                int nextday = nextdaydate.Day;
                int nextmonth = nextdaydate.Month;
                int nextyear = nextdaydate.Year;
                var nextdaymydate = this.GetMyDateFromList(nextday, nextmonth, nextyear, mydateslist);
                var datedto = helpermethods.GetDate(nextdaymydate, deskid,reservationid);
                calendar.Add(datedto);
            }
            return calendar;
        }
        public MyCalendarEditDTO GetExistingCalendar(int id,int day,int month,int year,int weeks)
        {
            Reservation reservation = (Reservation) this._reservationService.GetById(id);
            MyCalendarEditDTO calendardto = new MyCalendarEditDTO();
            calendardto.BeginDate = reservation.BeginDate;
            calendardto.EndDate = reservation.EndDate;
            calendardto.ReservationId = reservation.Id;
            int beginDay=day;
            int beginMonth=month;
            int beginYear=year;
            calendardto.MyDates = GetMyDatesList(beginDay, beginMonth, beginYear, weeks,reservation.Desk.Id,reservation.Id,new UpdateReservationMethods());
            calendardto.DeskLocationName = reservation.Desk.Location.Name;
            calendardto.DeskName = reservation.Desk.Name;
            calendardto.DeskId = reservation.Desk.Id;
            return calendardto;
        }
        public MyCalendarDTO GetCalendar(int beginDay,int beginMonth,int beginYear,int weeks,Desk desk)
        {
             MyCalendarDTO calendarDTO = new MyCalendarDTO();
             calendarDTO.DeskId = desk.Id;
             calendarDTO.DeskName = desk.Name;
             calendarDTO.DeskLocationName = desk.Location.Name;
             calendarDTO.MyDates = GetMyDatesList(beginDay,beginMonth,beginYear,weeks,desk.Id,0,new CreateReservationMethods());
             return calendarDTO;
        }
        
        public ServiceOperationStatusObject PersistReservation(string begindatestr,string enddatestr,string userid, IPersistReservationHelperMethods collissionMethod,Reservation reservation,Desk desk)
        {
            if(desk.Available==false)
            {
                return ServiceOperationStatusObject
                    .GetOperationStatusObject(
                        new CreateOperationMessage(),
                        new DeskIsNotAvailableMessage()
                    );
            }
            MyDate begindate = this._dateService.GetByDay(begindatestr);
            MyDate enddate = this._dateService.GetByDay(enddatestr);
            int totaldays = (int)(enddate.Date - begindate.Date).TotalDays;
            if (totaldays>7)
            {
                return ServiceOperationStatusObject
                    .GetOperationStatusObject(
                        new CreateOperationMessage(),
                        new WrongDatePeriodMessage()
                    );
            }else if(totaldays < 0)
            {
                return ServiceOperationStatusObject
                    .GetOperationStatusObject(
                        new CreateOperationMessage(),
                        new WrongDatePeriodMessage()
                    );
            }
            reservation.BeginDate = begindatestr;
            reservation.EndDate = enddatestr;
            reservation.DeskId= desk.Id;
            desk.Reservations.Add(reservation);
            reservation.OwnerId = userid;
            List<MyDate> newdates = new List<MyDate>();
            var mydateslist = this._dateService.GetList();
            for(int i=0;i<=totaldays;++i)
            {
                DateTime nextdaydate = begindate.Date.AddDays(i);
                int nextday = nextdaydate.Day;
                int nextmonth = nextdaydate.Month;
                int nextyear = nextdaydate.Year;
                MyDate nextdaymydate = this.GetMyDateFromList(nextday, nextmonth, nextyear, mydateslist);
                bool status = collissionMethod.DoesCollide(nextdaymydate, reservation);
                if(status==true)
                {
                    return ServiceOperationStatusObject
                    .GetOperationStatusObject(
                        new CreateOperationMessage(),
                        new AlreadyReservedMessage()
                    );
                }
                newdates.Add(nextdaymydate);
            }
            collissionMethod.Persist(_reservationService,reservation,newdates);
            return ServiceOperationStatusObject
                    .GetOperationStatusObject(
                        new CreateOperationMessage(),
                        new SucceededMessage()
                    );
        }
        public MyCalendarDTO GetEmptyCalendar(string msg)
        {
            MyCalendarDTO emptycalendar = new MyCalendarDTO();
            emptycalendar.DeskName = msg;
            emptycalendar.MyDates = new List<MyDateDTO>();
            emptycalendar.DeskLocationName = msg;
            return emptycalendar;
        }
        public MyCalendarEditDTO GetEmptyCalendarForEdit(string msg)
        {
            MyCalendarEditDTO emptycalendar = new MyCalendarEditDTO();
            emptycalendar.DeskName = msg;
            emptycalendar.MyDates = new List<MyDateDTO>();
            emptycalendar.DeskLocationName = msg;
            return emptycalendar;
        }
        public ServiceOperationStatusObject IsFurtherThan24H(int? id)
        {
            var status = this._reservationService.DoesExist(id);
            if(status.hasSucceeded==false)
            {
                return status;
            }
            Reservation reservation = (Reservation)this._reservationService.GetById(id);
            DateTime begindate;
            bool datestatus = DateTime.TryParse(reservation.BeginDate, out begindate);
            DateTime datenow = DateTime.Now;
            int daysbetween = (int)(datenow - begindate).TotalDays;
            if(daysbetween<1)
            {
                return ServiceOperationStatusObject
                    .GetOperationStatusObject(
                    new GetOperationMessage(),
                    new ExpectedDateToCloseMessage()
                    );
            }
            return ServiceOperationStatusObject
                .GetOperationStatusObject(
                    new GetOperationMessage(),
                    new SucceededMessage()
                );
        }
    }
}
