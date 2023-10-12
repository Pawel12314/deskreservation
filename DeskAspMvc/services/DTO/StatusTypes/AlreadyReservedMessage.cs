namespace DeskAspMvc.services.DTO.StatusTypes
{
    public class AlreadyReservedMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "The desk is arleady reserved for a given date";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
