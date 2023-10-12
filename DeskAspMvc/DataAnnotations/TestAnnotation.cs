using DeskAspMvc.Areas.Admin.Controllers;
using DeskAspMvc.services.Services2;
using System.ComponentModel.DataAnnotations;

namespace DeskAspMvc.DataAnnotations
{
    public class TestAnnotation : ValidationAttribute
    {
        private LocationService _locationService { get; set; }
        public TestAnnotation(LocationService _locationService)
        {
            this._locationService = _locationService;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(this._locationService.GetList().Count<6)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Too many elements in database");
        }
    }
}
