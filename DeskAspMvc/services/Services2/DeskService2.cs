using DeskAspMvc.Data;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using DeskModel.Models;
using Microsoft.EntityFrameworkCore;

namespace DeskAspMvc.services.Services2
{
    public class DeskService2 : BasicService
    {
        public DeskService2(ApplicationDbContext context) : base(context)
        {
        }

        public override void _Delete(IModel entry)
        {
            this._context.desks.Remove((Desk)entry);
        }

        public override ServiceOperationStatusObject _DeleteReqs(int? id)
        {
            return ServiceOperationStatusObject
                .getOperationStatusObject(
                new DeleteOperationMessage(), 
                new SucceededMessage());
        }

        protected override void _Create(IModel entry)
        {
            Desk _entry = (Desk)entry;
            /*if(_entry.locationKey!=null)
            {
                
            }*/
            this._context.desks.Add(_entry);
        }

        protected override bool _DoesExist(int id)
        {
            return _context.desks.Where(x => x.id == id).Any();
        }

        protected override void _Edit(IModel entry)
        {
            this._context.desks.Update((Desk)entry);
        }

        protected override IModel? _GetById(int? id)
        {
            if (id == null)
                return null;
            return this._context.desks.Include(x => x.location).FirstOrDefault(x => x.id == (id ?? -1));
        }
        public Desk GetByIdWithLocation(int? id)
        {
            if (id == null)
                return null;
            return this._context.desks.Include(x=>x.location).FirstOrDefault(x=>x.id==(id??-1));
        }
        protected override List<IModel> _GetList()
        {
            return null;
        }
        public new List<Desk> GetList()
        {
            return this._context.desks.ToList();
        }
        public ServiceOperationStatusObject SetDeskLocation(Desk desk, Location location)
        {
            desk.location = location;
            desk.locationKey = location.id;
            this._context.desks.Update(desk);
            this._context.SaveChanges();
            var status = ServiceOperationStatusObject
                .getOperationStatusObject(
                new SetDeskLocationMessage(), new SucceededMessage()
                );
            return status;
        }
        public ServiceOperationStatusObject RemoveDeskLocation(Desk desk)
        {
            desk.location = null;
            desk.locationKey = null;
            this._context.desks.Update(desk);
            this._context.SaveChanges();
            var status = ServiceOperationStatusObject
                .getOperationStatusObject(
                    new RemoveDeskLocationMessage(),
                    new SucceededMessage()
                );
            return status;
        }
        public new List<Desk> GetListWithLocation()
        {
            return this._context.desks.Include(x => x.location).ToList();
        }
    }
}
