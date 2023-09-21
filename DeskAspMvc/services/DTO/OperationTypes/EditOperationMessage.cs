namespace DeskAspMvc.services.DTO.OperationTypes
{
    public class EditOperationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking edit operation: ";
        }
    }
}
