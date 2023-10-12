using Antlr.Runtime.Tree;
using DeskAspMvc.Models.dto;
using DeskAspMvc.Models.Models;
using DeskAspMvc.services.DTO;
using Microsoft.Ajax.Utilities;

namespace DeskAspMvc.services.Services2.Methods
{
    public interface IPersistReservationHelperMethods
    {
        public void Persist(ReservationService service, Reservation reservation, List<MyDate> dates);
        public bool DoesCollide(MyDate date, Reservation reservation);
        public MyDateDTO GetDate(MyDate mydate, int deskid,int reservationid);
           
    }
}
