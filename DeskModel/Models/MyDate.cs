using System.ComponentModel.DataAnnotations;

namespace DeskModel.Models
{
    public class MyDate
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
        public int id { get; set; }
        public List<Reservation> reservations { get; set; }
    }
}
