using DeskAspMvc.Data;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using DeskAspMvc.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DeskAspMvc.services.Services2
{
    public class DeskService : BasicService
    {
        public DeskService(ApplicationDbContext context) : base(context)
        {
        }

        public override void _Delete(IModel entry)
        {
            this._context.desks.Remove((Desk)entry);
        }

        public override ServiceOperationStatusObject _DeleteReqs(int? id)
        {
            bool doesExist = this._context.desks.Any(desk => desk.Id == (id ?? -1));
            if(doesExist==false)
            {
                return ServiceOperationStatusObject
                .GetOperationStatusObject(
                new DeleteOperationMessage(),
                new NotFoundMessage());
            }
            Desk? desk = this._context.desks
                .Include(desk=>desk.Reservations)
                .Where(desk=>desk.Id==(id ?? -1)).FirstOrDefault();
            if (desk == null)
            {
                return ServiceOperationStatusObject
                .GetOperationStatusObject(
                new DeleteOperationMessage(),
                new NotFoundMessage());
            }
            if(desk.Reservations.Count!=0)
            {
                return ServiceOperationStatusObject
                .GetOperationStatusObject(
                new DeleteOperationMessage(),
                new NotEmptyDeskMessage());
            }

            return ServiceOperationStatusObject
                .GetOperationStatusObject(
                new DeleteOperationMessage(), 
                new SucceededMessage());
        }

        protected override void _Create(IModel entry)
        {
            Desk _entry = (Desk)entry;
            /*if(_entry.locationKey!=null)
            {
                
            }*/
            /*if(_entry.location==null && _entry.locationKey==null)
            {
                Location loc = new Location();
                loc.name = "testing for cascade insert";
                _entry.location = loc;

            }*/

            this._context.desks.Add(_entry);
        }

        protected override bool _DoesExist(int id)
        {
            return _context.desks.Where(x => x.Id == id).Any();
        }

        protected override void _Edit(IModel entry)
        {
            this._context.desks.Update((Desk)entry);
        }

        protected override IModel? _GetById(int? id)
        {
            if (id == null)
                return null;
            return this._context.desks.Include(x => x.Location).Include(x=>x.Reservations).FirstOrDefault(x => x.Id == (id ?? -1));
        }
        public Desk GetByIdWithLocation(int? id)
        {
            if (id == null)
                return null;
            return this._context.desks.Include(x=>x.Location?? new Location())
                .FirstOrDefault(x=>x.Id==(id??-1))??new Desk();
        }
        public Desk GetByIdWithLocationAndReservationsAndDates(int? id)
        {
            if(id==null)
            {
                return null;
            }
            return this
                ._context
                .desks
                .Include(desk => (desk.Location??new Location()))
                .Include(desk => desk.Reservations?? new List<Reservation>())
                .ThenInclude(reservation => reservation.Dates)
                .FirstOrDefault(x => x.Id == (id ?? -1)) ?? new Desk();
        }
        protected override List<IModel> _GetList()
        {
            return null;
        }
        public new List<Desk> GetList()
        {
            return this._context.desks.Include(desk=>desk.Location).ToList();
        }
        public ServiceOperationStatusObject SetDeskLocation(Desk desk, Location location)
        {
            desk.Location = location;
            desk.LocationKey = location.Id;
            this._context.desks.Update(desk);
            this._context.SaveChanges();
            var status = ServiceOperationStatusObject
                .GetOperationStatusObject(
                new SetDeskLocationMessage(), new SucceededMessage()
                );
            return status;
        }
        public ServiceOperationStatusObject RemoveDeskLocation(Desk desk)
        {
            desk.Location = null;
            desk.LocationKey = null;
            this._context.desks.Update(desk);
            this._context.SaveChanges();
            var status = ServiceOperationStatusObject
                .GetOperationStatusObject(
                    new RemoveDeskLocationMessage(),
                    new SucceededMessage()
                );
            return status;
        }
        public new List<Desk> GetListWithLocation()
        {
            return this._context.desks.Include(x => x.Location).ToList();
        }
    }
}
