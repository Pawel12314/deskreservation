using DeskAspMvc.Areas.Admin.Controllers;
using DeskAspMvc.Areas.Employee.Controllers;
using DeskAspMvc.Data;
using DeskAspMvc.Models.Models;
using DeskAspMvc.services.Services2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace deskmtest3
{
    [TestClass]
    public class ServicesTests
    {
        private ApplicationDbContext _context { get; set; }

        private LocationService _locationService { get; set; }
        private DeskService _deskService { get; set; }
        private ReservationService _reservationService { get; set; }
        private MyDateService _myDateService { get; set; }
        private ReservationServiceAdapter _reservationServiceAdapter { get; set; }
        private Reservation2Controller _reservation2Controller { get;set; }
        private Desk2Controller _desk2Controller { get;set; }

        [TestMethod]
        public void TestLocationService()
        {
            string testlocationame = "test location";
            Location location = new Location();
            location.Name = testlocationame;
            this._locationService.Create(location);
            Assert.IsTrue(this._locationService.GetList().Any(x => x.Name.Equals(testlocationame)));
        }


        [TestInitialize]
        public async Task initbeforeeach()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            this._context = context;
            this._locationService = new LocationService(_context);
            this._deskService = new DeskService(_context);
            this._reservationService = new ReservationService(_context);
            this._myDateService = new MyDateService(_context);
            this._reservationServiceAdapter = 
                new ReservationServiceAdapter(this._myDateService, 
                this._reservationService, 
                this._deskService);
            this._reservation2Controller =
                new Reservation2Controller(
                    this._reservationService,
                    this._deskService,
                    this._reservationServiceAdapter
                    );
            this._desk2Controller =
                new Desk2Controller(
                    this._deskService,
                    this._locationService
                    );
        }
        [TestMethod]
        public void TestDeskController()
        {
            string deskname = "name";
            Desk desk = new Desk();
            desk.Name = deskname;
            this._desk2Controller.Create(desk);
            Assert.IsTrue(this._deskService.GetList().Count==1);
            Assert.IsTrue(this._deskService.GetList().Any(desk => desk.Name.Equals(deskname)));
        }

    }
}
