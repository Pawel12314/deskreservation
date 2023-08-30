using DeskAspMvc.Data;
using DeskAspMvc.Models;
using DeskAspMvc.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DeskAspMvc.Areas.Employee.Controllers
{

    [Area("Employee")]
    public class ReservationController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public ReservationController(ApplicationDbContext con)
        {
            this._context = con;
        }
        public IActionResult Index()
        {
            var list = _context.desks.Include(x=>x.location).Include(x=>x.reservation).ToList();
            var dtolist = new List<EmployeeDeskDTO>();
            foreach(var desk in list)
            {
                if(desk.location==null)
                {
                    continue;
                }
                EmployeeDeskDTO dto = new EmployeeDeskDTO();
                dto.id = desk.id;
                dto.LocationName = desk.location.name;
                dto.Name = desk.name;
                dto.isAvailable = desk.reservation == null & desk.available;
                dtolist.Add(dto);
            }
            return View(dtolist);
        }
        public IActionResult Reserve(int? id)
        {
            bool deskexist = _context.desks.Where(x=>x.id==id).Any();
            if(id==null|| deskexist==false)
            {
                return RedirectToAction("Index");
            }
            
            Desk d = _context.desks.Where(x => x.id == id).Include(x=>x.location).SingleOrDefault();
            
            Reservation r = new Reservation();
            r.desk = d;
            string loginid = _context.Users.Where(x => x.Email.Equals(User.Identity.Name)).SingleOrDefault().Id;
            r.ownerId = loginid;

            return View(r);
        }


        [HttpPost]
        public IActionResult Reserve(Reservation reservation)
        {
            Desk d = _context.desks.Where(x => x.id == reservation.deskId).Include(x => x.location).SingleOrDefault();
            //Reservation r = new Reservation();
            reservation.desk = d;

            if (ModelState.IsValid)
            {
                if(DateTime.Now>reservation.date)
                {
                    return View(reservation);
                }
                if((reservation.date - DateTime.Now).TotalDays<2)
                {
                    return View(reservation);
                }
                _context.reservations.Add(reservation);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservation);
            
        }
        public IActionResult YourReservations()
        {
            string loginid = _context.Users.Where(x => x.Email.Equals(User.Identity.Name)).SingleOrDefault().Id;
            var list = _context
                .reservations
                .Where(x => x.ownerId.Equals(loginid))
                .Include(x => x.desk)
                .ThenInclude(x => x.location);
            return View(list);
        }

        public IActionResult RemoveReservation(int? id)
        {
            string loginid = _context.Users.Where(x => x.Email.Equals(User.Identity.Name)).SingleOrDefault().Id;
            var res = _context.reservations.Where(x => x.id == id).SingleOrDefault();
            if(res.ownerId.Equals(loginid)==false)
            {
                return RedirectToAction("YourReservations");
            }
            _context.reservations.Remove(res);
            _context.SaveChanges();
            return RedirectToAction("YourReservations");

        }
    }
}
