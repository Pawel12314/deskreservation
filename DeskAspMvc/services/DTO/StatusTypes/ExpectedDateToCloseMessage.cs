namespace DeskAspMvc.services.DTO.StatusTypes
{
    public sealed class ExpectedDateToCloseMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "Expected date is to close";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
