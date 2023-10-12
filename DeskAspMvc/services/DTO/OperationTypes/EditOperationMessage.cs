namespace DeskAspMvc.services.DTO.OperationTypes
{
    public sealed class EditOperationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking edit operation: ";
        }
    }
}
