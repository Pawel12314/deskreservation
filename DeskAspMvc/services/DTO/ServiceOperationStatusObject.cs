using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;

namespace DeskAspMvc.services.DTO
{
    public sealed class ServiceOperationStatusObject
    {
        public bool hasSucceeded { get; set; }
        public string message { get; set; }
        
        public static ServiceOperationStatusObject GetOperationStatusObject(IOperationMessage operation, IStatusTypeMessage error)
        {
            ServiceOperationStatusObject status = new ServiceOperationStatusObject();
            status.hasSucceeded = error.GetSuccessState();

            status.message = operation.GetMessage() + error.GetMessage();
            return status;
        }
        
    }
}
