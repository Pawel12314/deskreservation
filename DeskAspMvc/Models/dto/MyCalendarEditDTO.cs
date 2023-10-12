using System.ComponentModel;

namespace DeskAspMvc.Models.dto
{
    public sealed class MyCalendarEditDTO : MyCalendarDTO
    {
        [DisplayName("Starting date of reservation")]
        public string BeginDate { get; set; }
        [DisplayName("Ending date of reservation")]
        public string EndDate { get; set; }
        public int ReservationId { get; set; }
    }
}
