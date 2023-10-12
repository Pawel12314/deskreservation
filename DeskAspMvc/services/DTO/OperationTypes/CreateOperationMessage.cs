namespace DeskAspMvc.services.DTO.OperationTypes
{
    public sealed class CreateOperationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking create operation: ";
        }
    }
}
