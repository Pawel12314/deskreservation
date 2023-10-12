using DeskAspMvc.Models.dto;
using DeskAspMvc.Models.Models;

namespace DeskAspMvc.services.Services2.Methods
{
    public class UpdateReservationMethods : IPersistReservationHelperMethods
    {
        public bool DoesCollide(MyDate date, Reservation reservation)
        {
            bool isdeskincurrentreservation = date
                .Reservations
                .Any(res =>
                    res.DeskId == reservation.DeskId
                    && res.OwnerId == reservation.OwnerId
                    );
            if(isdeskincurrentreservation==true)
            {
                return false;
            }
            bool isdeskinanyreservation = date
                .Reservations
                .Any(
                    res =>
                    res.DeskId == reservation.DeskId
                );

            return isdeskinanyreservation;
        }

        public void Persist(ReservationService service, Reservation reservation, List<MyDate> dates)
        {
            foreach(var date in reservation.Dates)
            {
                date.Reservations.Remove(reservation);
            }
            service.Edit(reservation, reservation.Id);
            foreach (var date in dates)
            {
                date.Reservations.Add(reservation);
                reservation.Dates.Add(date);
            }
            
            service.Edit(reservation,reservation.Id);
        }

        public MyDateDTO GetDate(MyDate mydate, int deskid,int reservationid)
        {
            MyDateDTO mydatedto = new MyDateDTO();
            mydatedto.Year = mydate.Date.Year;
            mydatedto.Month = mydate.Date.Month;
            mydatedto.Day = mydate.Date.Day;
            mydatedto.DayId = mydate.Id;
            mydatedto.Weekday = mydate.Date.DayOfWeek;
            if (mydate.Reservations == null)
            {
                mydatedto.IsReserved = false;
            }
            else
            {
                mydatedto.IsReserved = mydate.Reservations.Any(res => res.DeskId == deskid);
            }
            mydatedto.IsInCurrentReservation = mydate.Reservations
                .Where(res=>res.DeskId==deskid)
                .Any(res=>res.Id==reservationid);
            return mydatedto;
        }

    }
}
