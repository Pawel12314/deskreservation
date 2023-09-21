using DeskAspMvc.Data;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using DeskAspMvc.services.DTO;
using DeskModel.Models;

namespace DeskAspMvc.services
{
    public abstract  class BasicService
    {
        protected ApplicationDbContext _context { get; set; }
        public BasicService(ApplicationDbContext context)
        {
            this._context = context;
        }
        protected abstract List<IModel> _GetList();
        public virtual List<IModel> GetList()
        {
            return this._GetList();
        }
        /*public List GetList()
        {
            return this._context.Set<T>().ToList();
        }*/
        protected abstract void _Create(IModel entry);
        public ServiceOperationStatusObject Create(IModel entry)
        {
            this._Create(entry);
            _context.SaveChanges();
            ServiceOperationStatusObject status
                = ServiceOperationStatusObject
                .getOperationStatusObject(new CreateOperationMessage(), new SucceededMessage());
            return status;
        }
        protected abstract bool _DoesExist(int id);
        public ServiceOperationStatusObject DoesExist(int? id)
        {
            if (id == null)
            {
                var res
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new GetOperationMessage(), new IdNotProvidedMessage());
                return res;
            }
            var doesExistStatus = _DoesExist(id ?? 0);
            if (doesExistStatus == false)
            {
                var res
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new GetOperationMessage(), new NotFoundMessage());
                return res;
            }
            else
            {
                var res
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new GetOperationMessage(), new SucceededMessage());
                return res;
            }
        }
        protected abstract void _Edit(IModel entry);
        public ServiceOperationStatusObject Edit(IModel entry, int id)
        {

            if (_DoesExist(id))
            {
                //_context.locations.Update(entry);
                this._Edit(entry);
                _context.SaveChanges();
                ServiceOperationStatusObject ret
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new EditOperationMessage(), new SucceededMessage());
                return ret;
            }
            else
            {
                ServiceOperationStatusObject ret
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new EditOperationMessage(), new NotFoundMessage());
                return ret;
            }
        }
        protected abstract IModel? _GetById(int? id);
        public IModel? GetById(int? id)
        {
            if (id == null)
                return null;
            return this._GetById(id);
            //return this._context.locations.Find(id);
        }
        public  abstract ServiceOperationStatusObject _DeleteReqs(int? id);
        public abstract void _Delete(IModel entry);
        public ServiceOperationStatusObject Delete(int? id)
        {
            if (id == null)
            {
                var res
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new DeleteOperationMessage(), new IdNotProvidedMessage());
                return res;
            }
            if (_DoesExist(id ?? 0))
            {
                var status = this._DeleteReqs(id);
                if(status.hasSucceeded==false)
                {
                    return status;
                }
                IModel entry = this._GetById(id);
                this._Delete(entry);
                this._context.SaveChanges();
                return status;
                
            }
            else
            {
                ServiceOperationStatusObject ret
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new DeleteOperationMessage(), new NotFoundMessage());
                return ret;
            }
        }
    }
}
