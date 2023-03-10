using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NUnit.Framework;
using WebAPI.IntegrationTests.Framework.Repository;
using WebAPI.Repository;

namespace WebAPI.IntegrationTests.Framework;

public class FrameworkFixture
{
    protected virtual WebApplicationFactory<Program> SetUp()
    {
        Program.EnableDataMigration = false;
        DataContextFactory.DataContextType = typeof(InMemoryDataContext);
        
        var factory = new WebApplicationFactory<Program>();
        factory.WithWebHostBuilder(builder =>
            builder.ConfigureServices(configure =>
                configure.Replace(new ServiceDescriptor(typeof(DataContext), typeof(InMemoryDataContext)))));
        
        return factory;
    }

    [TearDown]
    protected virtual void TearDown()
    {
        Program.EnableDataMigration = true;
        DataContextFactory.DataContextType = typeof(DataContext);
    }
}