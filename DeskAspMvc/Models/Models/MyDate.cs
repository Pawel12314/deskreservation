using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeskAspMvc.Models.Models
{
    public class MyDate : IModel
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
