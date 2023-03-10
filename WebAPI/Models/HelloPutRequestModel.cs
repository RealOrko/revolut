using System.ComponentModel.DataAnnotations;
using WebAPI.Validation;

namespace WebAPI.Models;

public class HelloPutRequestDateModel
{
    [DataType(DataType.Date)] 
    [DateIsNowOrPast(ErrorMessage = "This field cannot be a date in the future")]
    [RegularExpression(@"^\d{4}-((0[1-9])|(1[012]))-((0[1-9]|[12]\d)|3[01])",
        ErrorMessage = "This field must be in the format YYYY-MM-DD")]
    public string DateOfBirth { get; set; }
}

public class HelloPutRequestUserModel : HelloPutRequestDateModel
{
    public HelloPutRequestUserModel(string username, string dateOfBirth)
    {
        Username = username;
        DateOfBirth = dateOfBirth!;
    }

    [RegularExpression("^[a-zA-Z]*$",
        ErrorMessage = "This field can only contain upper and lowercase alpha characters")]
    public string Username { get; }
}