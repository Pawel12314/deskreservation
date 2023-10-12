using DeskAspMvc.DataAnnotations;
using DeskAspMvc.services.Services2;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeskAspMvc.Models.Models
{
    public class Location : IModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100,ErrorMessage ="location name length should be lower than 100 characters")]
        [MinLength(3,ErrorMessage ="lenght of location name should not be lower than 3 characters")]
        [DisplayName("Name of location")]
        
        public string Name { get; set; }

        public List<Desk>? Desks { get; set; }

      
    }
}
