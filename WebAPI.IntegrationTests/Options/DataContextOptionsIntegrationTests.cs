using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using WebAPI.IntegrationTests.Framework;
using WebAPI.Options;

namespace WebAPI.IntegrationTests.Options;

public class DataContextOptionsIntegrationTests : FrameworkFixture
{
    private WebApplicationFactory<Program> _applicationFactory = null!;

    [SetUp]
    public void Setup()
    {
        _applicationFactory = base.SetUp();
    }

    [Test]
    public void ShouldGetDataContextOptionsCorrectly()
    {
        var configuration = (IConfiguration) _applicationFactory.Services.GetService(typeof(IConfiguration))!;
        var connectionString = configuration.GetSection("DataContext").Get<DataContextOptions>().ConnectionString;
        
        Assert.That(connectionString, Is.EqualTo($"Host=localhost; Port=5432; Database=postgres; Username=postgres; Password=postgres"));
    }
}