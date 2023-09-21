using DeskAspMvc.Data;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using DeskModel.Models;

namespace DeskAspMvc.services
{
    public class DeskService
    {
        private ApplicationDbContext _context { get; set; }
        public DeskService(ApplicationDbContext context) { 
            this._context = context;
        }
        public List<Desk> GetDesks()
        {
            return this._context.desks.ToList();
        }
        public ServiceOperationStatusObject Create(Desk desk)
        {
            this._context.desks.Add(desk);
            this._context.SaveChanges();
            var res
                = ServiceOperationStatusObject
                .getOperationStatusObject(new CreateOperationMessage(), new SucceededMessage());
            return res;
        }
        public ServiceOperationStatusObject Edit(Desk desk,int id)
        {
            return null;
        }
    }
}
