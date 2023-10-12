namespace DeskAspMvc.services.DTO.StatusTypes
{
    public sealed class DateNotFoundMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "provided date is not valid";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
