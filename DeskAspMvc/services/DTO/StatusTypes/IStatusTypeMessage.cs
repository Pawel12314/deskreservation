namespace DeskAspMvc.services.DTO.StatusTypes
{
    public interface IStatusTypeMessage
    {
        public abstract string GetMessage();
        public abstract bool GetSuccessState(); 
    }
}
