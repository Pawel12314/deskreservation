namespace DeskAspMvc.services.DTO.OperationTypes
{
    public class CreateOperationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking create operation: ";
        }
    }
}
