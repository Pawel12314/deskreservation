using System.ComponentModel.DataAnnotations;

namespace DeskAspMvc.Models
{
    public class Location
    {
        public int id { get; set; }
        [Required]
        [StringLength(100,ErrorMessage ="location name length should be lower than 100 characters")]
        [MinLength(3,ErrorMessage ="lenght of location name should not be lower than 3 characters")]
        public string name { get; set; }

        public List<Desk>? desks { get; set; }

      
    }
}
