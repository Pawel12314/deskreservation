using DeskAspMvc.Data;
//using DeskAspMvc.Models;
using DeskAspMvc.Models.Models;
using DeskAspMvc.services.Services2;
using Microsoft.EntityFrameworkCore;

namespace deskmtest3
{
    [TestClass]
    public class UnitTest1
    {
        private ApplicationDbContext _context { get; set; }

        


        [TestInitialize]
        public async Task initbeforeeach()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            this._context = context;
        
        }


        [TestMethod]
        public void TestWarsaw()
        {
            Location loc = new Location();
            loc.Name = "Warsaw";
            _context.locations.Add(loc);
            _context.SaveChanges();
            Assert.IsTrue(_context.locations.ToList().Count() == 1);
            Assert.IsTrue(_context.locations.ToList().First().Name.Equals("Warsaw"));

        }
        [TestMethod]
        public void IsValidDateValid()
        {
            DateTime dt = new DateTime();
            string dtstr = "12/02/2023";
            bool doesexist = DateTime.TryParse(dtstr, out dt);
            Assert.IsTrue(doesexist == true);
        }
        [TestMethod]
        public void IsValidDateValid2()
        {
            DateTime dt = new DateTime();
            string dtstr = "12/2/2023";
            bool doesexist = DateTime.TryParse(dtstr, out dt);
            Assert.IsTrue(doesexist == true);
        }
        [TestMethod]
        public void IsInvalidDateInvalid()
        {
            DateTime dt = new DateTime();
            string dtstr = "45/02/2023";
            bool doesexist = DateTime.TryParse(dtstr, out dt);
            Assert.IsTrue(doesexist == false);
        }
        
        /*[TestMethod]
        public void InvalidDateShouldFail()
        {
            DateTime dt = new DateTime();
            string dtstr = "32/11/2023";
            bool doesexist = DateTime.TryParse(dtstr, out dt);
            Assert.IsTrue(doesexist == true);
        }*/
        [TestMethod]
        public void TestBerlin()
        {
            Location loc = new Location();
            loc.Name = "Berlin";
            _context.locations.Add(loc);
            _context.SaveChanges();
            Assert.IsTrue(_context.locations.ToList().Count() == 1);
            Assert.IsTrue(_context.locations.ToList().First().Name.Equals("Berlin"));

        }
        [TestCleanup]
        public async Task Dispose()
        {
            this._context.Database.EnsureDeleted();
            this._context.Dispose();
        }
        [TestMethod]
        public void DoesReservationHaveGoodDayCount()
        {

        }

    }
}
