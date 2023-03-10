using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using WebAPI.Models;

namespace WebAPI.Tests.Models;

public class HelloGetRequestModelUnitTests
{
    [TestCase("john")]
    [TestCase("jill")]
    [TestCase("John")]
    [TestCase("Jill")]
    public void ShouldPassModelValidation(string username)
    {
        var model = new HelloGetRequestModel(username);
        var validation = new ValidationContext(model);
        
        Assert.IsTrue(Validator.TryValidateObject(model, validation, new List<ValidationResult>(), validateAllProperties:true));
    }
    
    [TestCase("john1", "Username", "This field can only contain upper and lowercase alpha characters")]
    [TestCase("1jill", "Username", "This field can only contain upper and lowercase alpha characters")]
    public void ShouldFailModelValidation(string username, string assertErrorField, string assertErrorMessage)
    {
        var model = new HelloGetRequestModel(username);
        var validation = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, validation, results, validateAllProperties:true);
        
        Assert.IsFalse(isValid);
        Assert.That(results.Select(x => x.ErrorMessage), Contains.Item(assertErrorMessage));
        Assert.That(results.SelectMany(x => x.MemberNames), Contains.Item(assertErrorField));
    }
}