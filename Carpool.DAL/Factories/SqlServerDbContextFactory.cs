using Microsoft.EntityFrameworkCore;

namespace Carpool.DAL.Factories
{
    public class SqlServerDbContextFactory : IDbContextFactory<CarpoolDbContext>
    {
        private readonly string _connectionString;
        private readonly bool _seedDemoData;

        public SqlServerDbContextFactory(string connectionString, bool seedDemoData = false)
        {
            _connectionString = connectionString;
            _seedDemoData = seedDemoData;
        }

        public CarpoolDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarpoolDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);


            return new CarpoolDbContext(optionsBuilder.Options, _seedDemoData);
        }
    }
}