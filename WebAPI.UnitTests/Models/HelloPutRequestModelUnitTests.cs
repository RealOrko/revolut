using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using WebAPI.Models;

namespace WebAPI.Tests.Models;

public class HelloPutRequestModelUnitTests
{
    [TestCase("john", "1983-02-05")]
    [TestCase("jill", "2001-06-30")]
    [TestCase("John", "1991-12-16")]
    [TestCase("Jill", "2010-08-15")]
    public void ShouldPassModelValidation(string username, string dateOfBirth)
    {
        var model = new HelloPutRequestUserModel(username, dateOfBirth);
        var validation = new ValidationContext(model);
        
        Assert.IsTrue(Validator.TryValidateObject(model, validation, new List<ValidationResult>(), validateAllProperties:true));
    }
    
    [TestCase("John", "2090-12-16", "DateOfBirth", "This field cannot be a date in the future")]
    [TestCase("Jill", "2010-8-15", "DateOfBirth", "This field must be in the format YYYY-MM-DD")]
    [TestCase("john1", "1983-02-05", "Username", "This field can only contain upper and lowercase alpha characters")]
    [TestCase("1jill", "2001-06-30", "Username", "This field can only contain upper and lowercase alpha characters")]
    public void ShouldFailModelValidation(string username, string dateOfBirth, string assertErrorField, string assertErrorMessage)
    {
        var model = new HelloPutRequestUserModel(username, dateOfBirth);
        var validation = new ValidationContext(model);
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, validation, results, validateAllProperties:true);
        
        Assert.IsFalse(isValid);
        Assert.That(results.Select(x => x.ErrorMessage), Contains.Item(assertErrorMessage));
        Assert.That(results.SelectMany(x => x.MemberNames), Contains.Item(assertErrorField));
    }
}