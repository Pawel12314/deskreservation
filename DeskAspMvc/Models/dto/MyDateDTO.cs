namespace DeskAspMvc.Models.dto
{
    public class MyDateDTO
    {
        public int DayId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsReserved { get; set; }
        public DayOfWeek Weekday { get; set; }
        public bool IsInCurrentReservation { get; set; }
    }
}
