using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Repository;

public class DataContextFactory : IDbContextFactory<DataContext>
{
    public static Type DataContextType = typeof(DataContext);

    private readonly IConfiguration _configuration;

    public DataContextFactory()
    {
    }

    public DataContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public virtual DataContext CreateDbContext()
    {
        return (DataContext)Activator.CreateInstance(DataContextType, _configuration);
    }
}