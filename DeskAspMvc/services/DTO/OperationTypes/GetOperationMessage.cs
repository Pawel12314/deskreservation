namespace DeskAspMvc.services.DTO.OperationTypes
{
    public sealed class GetOperationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking get operation: ";
        }
    }
}
