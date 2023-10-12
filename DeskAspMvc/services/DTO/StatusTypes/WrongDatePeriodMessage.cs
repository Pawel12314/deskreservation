namespace DeskAspMvc.services.DTO.StatusTypes
{
    public sealed class WrongDatePeriodMessage : IStatusTypeMessage
    {
        public string GetMessage()
        {
            return "Wrong date was provided";
        }

        public bool GetSuccessState()
        {
            return false;
        }
    }
}
