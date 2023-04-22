using Carpool.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Carpool.Common.Tests.Factories;

public class DbContextSQLiteTestingFactory : IDbContextFactory<CarpoolDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public CarpoolDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<CarpoolDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");


        return new CarpoolDbContext(builder.Options, _seedTestingData);
    }
}