namespace DeskAspMvc.services.DTO.StatusTypes
{
    public class NotFoundMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "element was not found";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
