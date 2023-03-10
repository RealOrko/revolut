using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models;

public class HelloGetRequestModel
{
    public HelloGetRequestModel(string username)
    {
        Username = username;
    }

    [RegularExpression("^[a-zA-Z]*$",
        ErrorMessage = "This field can only contain upper and lowercase alpha characters")]
    public string Username { get; }
}