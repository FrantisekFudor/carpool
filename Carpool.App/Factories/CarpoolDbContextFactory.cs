using Carpool.DAL;
using Microsoft.EntityFrameworkCore;

namespace Carpool.App.Factories
{
    public class CarpoolDbContextFactory : IDbContextFactory<CarpoolDbContext>
    {
        private readonly string _connectionString;

        public CarpoolDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CarpoolDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarpoolDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new CarpoolDbContext(optionsBuilder.Options);
        }
    }
}