using DeskAspMvc.Data;
using DeskAspMvc.Models;
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
            loc.name = "Warsaw";
            _context.locations.Add(loc);
            _context.SaveChanges();
            Assert.IsTrue(_context.locations.ToList().Count() == 1);
            Assert.IsTrue(_context.locations.ToList().First().name.Equals("Warsaw"));

        }
        [TestMethod]
        public void TestBerlin()
        {
            Location loc = new Location();
            loc.name = "Berlin";
            _context.locations.Add(loc);
            _context.SaveChanges();
            Assert.IsTrue(_context.locations.ToList().Count() == 1);
            Assert.IsTrue(_context.locations.ToList().First().name.Equals("Berlin"));

        }
        [TestCleanup]
        public async Task Dispose()
        {
            this._context.Database.EnsureDeleted();
            this._context.Dispose();
        }
    }
}
