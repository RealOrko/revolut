using WebAPI.Providers;

namespace WebAPI.Models;

public class HelloGetResponseModel
{
    public HelloGetResponseModel(string username, DateTime dateOfBirth)
    {
        Message = GetUserMessage(username, dateOfBirth);
    }

    public string Message { get; }

    private static string GetUserMessage(string username, DateTime dateOfBirth)
    {
        var nextBirthday = CalculateNextBirthday(dateOfBirth);
        var numberOfDays = (nextBirthday - DateTimeProvider.Now()).TotalDays;

        if (numberOfDays > 0)
        {
            return $"Hello, {username}! Your birthday is in {numberOfDays} day(s)";
        }
        
        return $"Hello, {username}! Happy birthday!";
    }
    
    private static DateTime CalculateNextBirthday(DateTime dateOfBirth)
    {
        var currentDate = DateTimeProvider.Now();

        var nextBirthDayYear = currentDate.Year;
        
        if (currentDate.Month > dateOfBirth.Month)
        {
            nextBirthDayYear++;
        }
        
        if (DateTimeProvider.Now().Month == dateOfBirth.Month && DateTimeProvider.Now().Day > dateOfBirth.Day)
        {
            nextBirthDayYear++;
        }
        
        return new DateTime(nextBirthDayYear, dateOfBirth.Month, dateOfBirth.Day);
    }
}