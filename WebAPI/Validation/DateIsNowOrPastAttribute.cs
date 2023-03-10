using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebAPI.Validation;

public class DateIsNowOrPastAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null) return false;
        return DateTime.TryParseExact(value.ToString()!, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                   DateTimeStyles.None, out var dateTime)
               && dateTime <= DateTime.Today;
    }
}