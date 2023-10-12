namespace DeskAspMvc.services.DTO.StatusTypes
{
    public sealed class DeskHasNoAssignedLocationMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "Desk has no assigned location";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
