namespace DeskAspMvc.services.DTO.StatusTypes
{
    public class IdNotProvidedMessage : IStatusTypeMessage
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
