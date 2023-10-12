using DeskAspMvc.Models.dto;
using DeskAspMvc.Models.Models;

namespace DeskAspMvc.services.Services2.Methods
{
    public class CreateReservationMethods : IPersistReservationHelperMethods
    {
        public bool DoesCollide(MyDate date, Reservation reservation)
        {
            bool isdeskinanyreservation = date
                .Reservations
                .Any(res =>
                    res.DeskId == reservation.DeskId
                    );
            return isdeskinanyreservation;

        }

        public MyDateDTO GetDate(MyDate mydate, int deskid, int reservationid)
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
            mydatedto.IsInCurrentReservation = false;
            return mydatedto;
        }

  

        public void Persist(ReservationService service, Reservation reservation, List<MyDate> dates)
        {
            reservation.Dates = dates;
            service.Create(reservation);
        }

        
    }
}
