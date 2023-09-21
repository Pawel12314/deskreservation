using DeskAspMvc.Data;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using DeskModel.Models;

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
            this._context.desks.Add((Desk)entry);
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
            return this._context.desks.Find(id);
        }

        protected override List<IModel> _GetList()
        {
            return null;
        }
        public new List<Desk> GetList()
        {
            return this._context.desks.ToList();
        }
    }
}
