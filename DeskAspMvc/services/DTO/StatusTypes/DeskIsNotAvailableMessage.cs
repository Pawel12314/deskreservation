namespace DeskAspMvc.services.DTO.StatusTypes
{
    public sealed class DeskIsNotAvailableMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "Desk is not available";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
