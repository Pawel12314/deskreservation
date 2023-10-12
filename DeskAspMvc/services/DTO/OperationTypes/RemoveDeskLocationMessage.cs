namespace DeskAspMvc.services.DTO.OperationTypes
{
    public sealed class RemoveDeskLocationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking remove desk location operation: ";
        }
    }
}
