using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeskModel.Models
{
    public class Desk : IModel
    {
        [Required]
        [MinLength(3,ErrorMessage ="desk name should be longer than 3 characters")]
        [MaxLength(30,ErrorMessage ="desk name should be shorter than 30 characters")]
        public string name { get; set; }
        public int id { get; set; }
        [DisplayName("location of desk")]
        public int? locationKey { get; set; }
        public Location? location { get; set; }
        //public int? reservationId { get; set; }
        public List<Reservation>? reservations { get; set; }
        public bool available { get; set; }
    }
}
