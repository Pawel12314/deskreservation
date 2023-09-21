namespace DeskAspMvc.services.DTO.OperationTypes
{
    public class GetOperationMessage : IOperationMessage
    {
        public string GetMessage()
        {
            return "invoking get operation: ";
        }
    }
}
