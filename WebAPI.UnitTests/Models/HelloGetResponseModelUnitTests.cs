using System;
using NUnit.Framework;
using WebAPI.Models;
using WebAPI.Providers;

namespace WebAPI.Tests.Models;

public class HelloGetResponseModelUnitTests
{
    [TearDown]
    public void TearDown()
    {
        DateTimeProvider.ResetDateTime();
    }
    
    [TestCase("gemma", "1999-01-01", "2000-01-01", "Hello, gemma! Happy birthday!")]
    [TestCase("gemma", "1978-01-01", "2000-01-01", "Hello, gemma! Happy birthday!")]
    [TestCase("jack", "2000-01-02", "2000-01-01", "Hello, jack! Your birthday is in 1 day(s)")]
    [TestCase("jack", "2000-01-01", "2000-01-02", "Hello, jack! Your birthday is in 365 day(s)")]
    [TestCase("jill", "2000-01-01", "2000-12-31", "Hello, jill! Your birthday is in 1 day(s)")]
    [TestCase("jill", "2000-01-31", "2000-12-31", "Hello, jill! Your birthday is in 31 day(s)")]
    [TestCase("jeff", "1978-03-01", "2000-01-01", "Hello, jeff! Your birthday is in 60 day(s)")]
    [TestCase("jeff", "1978-04-01", "2000-01-01", "Hello, jeff! Your birthday is in 91 day(s)")]
    [TestCase("jeff", "1978-05-01", "2000-01-01", "Hello, jeff! Your birthday is in 121 day(s)")]
    public void ShouldReturnCorrectMessageField(string username, string birthDate, string nowDate, string assertMessage)
    {
        var nowDateTime = DateTime.Parse(nowDate);
        DateTimeProvider.SetDateTime(nowDateTime);
        
        var birthDateTime = DateTime.Parse(birthDate);
        var model = new HelloGetResponseModel(username, birthDateTime);
        
        Assert.That(model.Message, Is.EqualTo(assertMessage));
    }
}