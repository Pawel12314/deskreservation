using DeskAspMvc.Data;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using DeskModel.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace DeskAspMvc.services
{
    public class LocationService
    {
        private ApplicationDbContext _context { get; set; }
        public LocationService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public List<Location> GetList()
        {
            return this._context.locations.ToList();
        }
        public ServiceOperationStatusObject Create(Location location)
        {
            _context.locations.Add(location);
            _context.SaveChanges();
            ServiceOperationStatusObject ret 
                = ServiceOperationStatusObject
                .getOperationStatusObject(new CreateOperationMessage(),new SucceededMessage());
            return ret;
        }
        private bool _DoesExist(int id)
        {
            return _context.locations.Where(x=>x.id==id).Any();
        }
        public ServiceOperationStatusObject DoesExist(int? id)
        {
            if(id==null)
            {
                var res
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new GetOperationMessage(), new IdNotProvidedMessage());
                return res;
            }
            var doesExistStatus = _DoesExist(id??0);
            if(doesExistStatus==false)
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
        public ServiceOperationStatusObject Edit(Location location,int id)
        {
            
            if(_DoesExist(id))
            {
                _context.locations.Update(location);
                _context.SaveChanges();
                ServiceOperationStatusObject ret 
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new EditOperationMessage(),new SucceededMessage());
                return ret;
            }
            else
            {
                ServiceOperationStatusObject ret 
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new EditOperationMessage(),new NotFoundMessage());
                return ret;
            }
        }
        public Location? GetById(int? id)
        {
            if (id == null)
                return null;
            return this._context.locations.Find(id);
        }
        public ServiceOperationStatusObject Delete(int? id)
        {
            if (id == null)
            {
                var res
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new DeleteOperationMessage(), new IdNotProvidedMessage());
                return res;
            }
            if (_DoesExist(id??0))
            {
                Location location = _context.locations.Include(x=>x.desks).ToList().Find(x=>x.id==id);
                if(location.desks.Count()==0)
                {
                    _context.locations.Remove(location);
                    _context.SaveChanges();
                    ServiceOperationStatusObject ret
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new DeleteOperationMessage(), new SucceededMessage());
                    return ret;
                }
                else
                {
                    ServiceOperationStatusObject ret
                    = ServiceOperationStatusObject
                    .getOperationStatusObject(new DeleteOperationMessage(), new NotEmptyMessage());
                    return ret;
                }
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
