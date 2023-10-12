using System.ComponentModel;

namespace DeskAspMvc.Models.dto
{
    public class MyCalendarDTO
    {
        public List<MyDateDTO> MyDates { get; set; }
        public int DeskId { get; set; }
        [DisplayName("Desk name")]
        public string DeskName { get; set; }
        [DisplayName("Desks location name")]
        public string DeskLocationName { get; set; }
    }
}
