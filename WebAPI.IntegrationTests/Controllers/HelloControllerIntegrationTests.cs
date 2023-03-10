using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NUnit.Framework;
using WebAPI.IntegrationTests.Framework;
using WebAPI.Repository;

namespace WebAPI.IntegrationTests.Controllers;

public class HelloControllerIntegrationTests : FrameworkFixture
{
    private HttpClient _httpClient = null!;
    
    [SetUp]
    public void Setup()
    {
        _httpClient = base.SetUp().CreateClient();
    }
    
    [Test]
    public async Task ShouldPutAndGetCorrectly()
    {
        var putResult = await _httpClient.PutAsync("/hello/john", new StringContent("{\"dateOfBirth\":\"1980-01-12\"}", Encoding.UTF8, "application/json"));
        putResult.EnsureSuccessStatusCode();

        var getResult = await _httpClient.GetAsync("/hello/john");
        getResult.EnsureSuccessStatusCode();
    }
}