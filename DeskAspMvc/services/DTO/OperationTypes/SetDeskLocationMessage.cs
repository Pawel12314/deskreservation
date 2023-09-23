namespace DeskAspMvc.services.DTO.OperationTypes
{
    public class SetDeskLocationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking set desk location operation: ";
        }
    }
}
