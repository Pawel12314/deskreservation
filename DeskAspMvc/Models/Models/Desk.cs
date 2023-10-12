using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeskAspMvc.Models.Models
{
    public class Desk : IModel
    {
        [Required]
        [MinLength(3,ErrorMessage ="desk name should be longer than 3 characters")]
        [MaxLength(30,ErrorMessage ="desk name should be shorter than 30 characters")]
        [DisplayName("Name of desk")]
        public string Name { get; set; }
        public int Id { get; set; }
        [DisplayName("location of desk")]
        public int? LocationKey { get; set; }
        public Location? Location { get; set; }
        public List<Reservation>? Reservations { get; set; }
        [DisplayName("Is desk available")]
        public bool Available { get; set; }
    }
}
