using DGIIAPP.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DGIIAPP.Tests
{
    public class InMemoryDatabaseFixture
    {
        public ApplicationDbContext DbContext { get; private set; }

        public InMemoryDatabaseFixture()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            DbContext = new ApplicationDbContext(dbContextOptions);
            DbContext.Database.EnsureCreated();
        }
        
        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }
}
