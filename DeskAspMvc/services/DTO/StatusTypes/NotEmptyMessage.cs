namespace DeskAspMvc.services.DTO.StatusTypes
{
    public class NotEmptyMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "element is not empty, location shouldn't have any associated desk bedore deleting it";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
