using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI.Repository;

namespace WebAPI.IntegrationTests.Framework.Repository;

public class InMemoryDataContext : DataContext
{
    public InMemoryDataContext(IConfiguration configuration) : base(configuration)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("Any");
    }
}