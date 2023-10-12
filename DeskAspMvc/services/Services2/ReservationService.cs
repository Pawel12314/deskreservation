using DeskAspMvc.Data;
using DeskAspMvc.Models.Models;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using Microsoft.EntityFrameworkCore;

namespace DeskAspMvc.services.Services2
{
    public sealed class ReservationService : BasicService
    {
        public ReservationService(ApplicationDbContext context) : base(context)
        {
        }

        public override void _Delete(IModel entry)
        {
            this._context.reservations.Remove((Reservation)entry);
        }

        public override ServiceOperationStatusObject _DeleteReqs(int? id)
        {
            return ServiceOperationStatusObject
                .GetOperationStatusObject(
                new DeleteOperationMessage(),
                new SucceededMessage()
                );
        }

        protected override void _Create(IModel entry)
        {
            this._context.reservations.Add((Reservation)entry);
        }

        protected override bool _DoesExist(int id)
        {
            return this._context.reservations.Any(x=>x.Id==id);
        }

        protected override void _Edit(IModel entry)
        {
            this._context.reservations.Update((Reservation)entry);
        }

        protected override IModel? _GetById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return this._context.reservations.Include(x=>x.Desk).Where(x=>x.Id==id).Include(x=>x.Dates).FirstOrDefault();
        }
        public void DeleteRelatedDates(Reservation reservation)
        {
            this._context.reservations.RemoveRange(reservation);
            this._context.SaveChanges();
        }
        protected override List<IModel> _GetList()
        {
            throw new NotImplementedException();
        }
        public new List<Reservation> GetList()
        {
            return this._context.reservations.Include(x => x.Desk).ThenInclude(d=>d.Location).Include(x => x.Dates).ToList();
        }
        public new List<MyDate> GetCalendar(string begin)
        {
            return null; 
        }
    }
}
