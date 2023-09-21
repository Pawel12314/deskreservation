namespace DeskAspMvc.services.DTO.OperationTypes
{
    public class DeleteOperationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking delete operation: ";
        }
    }
}
