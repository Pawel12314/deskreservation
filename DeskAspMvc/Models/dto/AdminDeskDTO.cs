namespace DeskAspMvc.Models.dto
{
    public class AdminDeskDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string LocationName { get; set; }
        public bool isAvailable { get; set; }
        public string reservedForUsername { get; set; }
    }
}
