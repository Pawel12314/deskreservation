using System.ComponentModel.DataAnnotations;

namespace DeskModel.Models
{
    public class Reservation
    {
        public int id { get; set; }
        //public long dueInMilis { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
        public string ownerId { get; set; }
        public int deskId { get; set; } 
        public Desk? desk { get; set; }
        public List<MyDate> dates { get; set; }

    }
}
