namespace DeskAspMvc.services.DTO.OperationTypes
{
    public sealed class DeleteOperationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking delete operation: ";
        }
    }
}
