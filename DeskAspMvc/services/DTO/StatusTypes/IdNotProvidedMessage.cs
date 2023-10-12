namespace DeskAspMvc.services.DTO.StatusTypes
{
    public sealed class IdNotProvidedMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "Id was not provided";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
