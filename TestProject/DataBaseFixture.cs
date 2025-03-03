
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace TestProject
{
    public class DataBaseFixture
    {
        public ManagerDbContext Context { get; private set; }
        public DataBaseFixture()
        {
            var options = new DbContextOptionsBuilder<ManagerDbContext>()
            .UseSqlServer("Server=srv2\\pupils;Database=Tests;Trusted_Connection=True;")
            .Options;
            Context = new ManagerDbContext(options);
            Context.Database.EnsureCreated();

        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

    }
}