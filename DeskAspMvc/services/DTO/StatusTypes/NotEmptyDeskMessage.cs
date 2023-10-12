namespace DeskAspMvc.services.DTO.StatusTypes
{
    public sealed class NotEmptyDeskMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "element is not empty, desk shouldn't have any associated reservations bedore deleting it";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
