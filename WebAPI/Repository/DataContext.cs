using Microsoft.EntityFrameworkCore;
using WebAPI.Options;
using WebAPI.Repository.Entities;

namespace WebAPI.Repository;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    protected DataContext()
    {
    }

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetSection("DataContext").Get<DataContextOptions>().ConnectionString);
    }

    public virtual DbSet<Person> Persons { get; set; }
}