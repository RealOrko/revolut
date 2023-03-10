using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.Repository.Entities;

namespace WebAPI.Tests.Controllers;

public class HelloControllerUnitTests
{
    private HelloController _controller = null!;
    private Mock<DataContext> _mockDataContext = null!;
    private Mock<DataContextFactory> _mockDataContextFactory = null!;

    [SetUp]
    public void SetUp()
    {
        _mockDataContext = new Mock<DataContext>();
        _mockDataContextFactory = new Mock<DataContextFactory>();
        _mockDataContextFactory.Setup(x => x.CreateDbContext()).Returns(_mockDataContext.Object);        
        
        _controller = new HelloController(_mockDataContextFactory.Object);
    }
    
    [TestCase("jack", typeof(OkObjectResult))]
    [TestCase("jack1", typeof(BadRequestObjectResult))]
    public async Task ShouldGetCorrectly(string username, Type assertResult)
    {
        var persons = new List<Person>() { new() { Name = username } };
        _mockDataContext.Setup(x => x.Persons).ReturnsDbSet(persons);
        
        var result = await _controller.Get(username);
        
        Assert.IsInstanceOf(assertResult, result);
    }

    [TestCase("jack", "1980-01-12", typeof(StatusCodeResult))]
    [TestCase("jack1", "1980-01-12", typeof(BadRequestObjectResult))]
    [TestCase("jack", "1980-1-2", typeof(BadRequestObjectResult))]
    public async Task ShouldPutCorrectly(string username, string birthDate, Type assertResult)
    {
        _mockDataContext.Setup(x => x.Persons).ReturnsDbSet(new List<Person>());
        var result = await _controller.Put(username, new HelloPutRequestDateModel() { DateOfBirth = birthDate});
        
        Assert.IsInstanceOf(assertResult, result);
    }
}