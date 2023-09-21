namespace DeskAspMvc.services.DTO.StatusTypes
{
    public class SucceededMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "operation was successfull";
        }

        public bool GetSuccessState()
        {
            return true;
        }
    }
}
