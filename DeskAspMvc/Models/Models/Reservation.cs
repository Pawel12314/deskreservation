using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeskAspMvc.Models.Models
{
    public class Reservation : IModel
    {
        public int Id { get; set; }
        [DisplayName("Reservation owner")]
        public string OwnerId { get; set; }
        public int DeskId { get; set; } 
        public Desk? Desk { get; set; }
        public List<MyDate> Dates { get; set; }
        [DisplayName("Begin of reservation")]
        public string BeginDate { get; set; }
        [DisplayName("End of reservation")]
        public string EndDate { get; set; }

    }
}
