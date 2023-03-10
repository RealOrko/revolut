using NUnit.Framework;
using WebAPI.Validation;

namespace WebAPI.Tests.Validation;

public class DateIsNowOrPastAttributeUnitTests
{
    private DateIsNowOrPastAttribute _attribute = null!;

    [SetUp]
    public void SetUp()
    {
        _attribute = new DateIsNowOrPastAttribute();
    }
    
    [TestCase("1989-01-05")]
    [TestCase("2010-08-15")]
    public void ShouldBeValid(string date)
    {
        Assert.IsTrue(_attribute.IsValid(date));
    }

    [TestCase(null)]
    [TestCase("1980-1-1")]
    [TestCase("2080-01-01")]
    [TestCase(null)]
    public void ShouldNotBeValid(string date)
    {
        Assert.IsFalse(_attribute.IsValid(date));        
    }
}