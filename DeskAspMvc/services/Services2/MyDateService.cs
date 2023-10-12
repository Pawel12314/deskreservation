using DeskAspMvc.Data;
using DeskAspMvc.Models.Models;
using DeskAspMvc.services.DTO;
using DeskAspMvc.services.DTO.OperationTypes;
using DeskAspMvc.services.DTO.StatusTypes;
using Microsoft.EntityFrameworkCore;

namespace DeskAspMvc.services.Services2
{
    public class MyDateService : BasicService
    {
        public MyDateService(ApplicationDbContext context) : base(context)
        {
        }

        public override void _Delete(IModel entry)
        {
            this._context.mydates.Remove((MyDate) entry);
        }

        public override ServiceOperationStatusObject _DeleteReqs(int? id)
        {
            return ServiceOperationStatusObject.GetOperationStatusObject(
                new DeleteOperationMessage(), new SucceededMessage()
                ); ;
        }

        protected override void _Create(IModel entry)
        {
            this._context.mydates.Add((MyDate)entry);
        }

        protected override bool _DoesExist(int id)
        {
            return _context.mydates.Where(x => x.Id == id).Any();
        }

        protected override void _Edit(IModel entry)
        {
            this._context.mydates.Update((MyDate)entry);
        }

        protected override IModel? _GetById(int? id)
        {
            return this._context.mydates.Find(id ?? 0);
        }

        protected override List<IModel> _GetList()
        {
            return null;
        }
        public new List<MyDate> GetList()
        {
            return this._context.mydates.Include(date=>date.Reservations).ThenInclude(res=>res.Desk).ToList();
        }

        public MyDate GetByDay(string datestr)
        {
            int day, month, year;
            int.TryParse(datestr.Split("/")[0],out day);
            int.TryParse(datestr.Split("/")[1],out month);
            int.TryParse(datestr.Split("/")[2],out year);
            return GetByDay(day, month, year);
        }
        public MyDate GetByDay(int day,int month,int year)
        {
            string strdate = day.ToString() + "/" + month.ToString() + "/" + year.ToString();
            DateTime localdate = new DateTime();
            bool isdateValid = DateTime.TryParse(strdate, out localdate);
            if(isdateValid==false)
            {
                return null;
            }
                        
            MyDate localmydate = new MyDate();
            localmydate.Date = localdate;
            
            MyDate? date = this._context
                .mydates
                .Include(
                x=>x.Reservations
                ).ThenInclude(
                x=>x.Desk
                )
                .Where(
                x => x.Date.Day == day
                ).Where(
                x => x.Date.Month == month
                ).Where(
                x => x.Date.Year == year
                )
                .FirstOrDefault() ?? localmydate;
            return date;
        }

        public List<MyDate> GetDayDesks(int? dateid)
        {
            return this._context
                .mydates
                .Include(x => x.Reservations)
                .ThenInclude(x => x.Desk)
                .Where(x => x.Id == (dateid ?? 0)).ToList();   
        }
    }
}
