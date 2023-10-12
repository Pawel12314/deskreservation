using DeskAspMvc.Data;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using DeskAspMvc.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DeskAspMvc.services.Services2
{
    public class LocationService : BasicService
    {
        public LocationService(ApplicationDbContext context) : base(context)
        {
        }

        public override void _Delete(IModel entry)
        {
            this._context.locations.Remove((Location)entry);
        }

        public override ServiceOperationStatusObject _DeleteReqs(int? id)
        {
            if (_DoesExist(id ?? 0)==false)
            {
                ServiceOperationStatusObject ret
                    = ServiceOperationStatusObject
                    .GetOperationStatusObject(new DeleteOperationMessage(), new NotFoundMessage());
                return ret;
            }
            var location = this._context.locations.Include(x => x.Desks).ToList().Find(x=>x.Id==(id??0));
            
            if (location.Desks.Count() == 0)
            {
                ServiceOperationStatusObject ret
                = ServiceOperationStatusObject
                .GetOperationStatusObject(new DeleteOperationMessage(), new SucceededMessage());
                return ret;
            }
            else
            {
                ServiceOperationStatusObject ret
                = ServiceOperationStatusObject
                .GetOperationStatusObject(new DeleteOperationMessage(), new NotEmptyLocationMessage());
                return ret;
            }
        }

        protected override void _Create(IModel entry)
        {
            this._context.locations.Add((Location)entry);
        }

        protected override bool _DoesExist(int id)
        {
            return _context.locations.Where(x => x.Id == id).Any();
        }

        protected override void _Edit(IModel entry)
        {
            this._context.locations.Update((Location)entry);
        }

        protected override IModel? _GetById(int? id)
        {
            if (id == null)
                return null;
            return this._context.locations.Find(id);
        }

        protected override List<IModel> _GetList()
        {
            return null;
        }

        public new List<Location>GetList()
        {
            return this._context.locations.ToList();
        }
        
    }
}
