namespace DeskAspMvc.services.DTO.OperationTypes
{
    public sealed class SetDeskLocationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking set desk location operation: ";
        }
    }
}
