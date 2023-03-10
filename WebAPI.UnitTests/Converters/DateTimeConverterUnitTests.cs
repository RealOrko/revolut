using System;
using System.IO;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using WebAPI.Converters;

namespace WebAPI.Tests.Converters;

public class DateTimeConverterUnitTests
{
    [TestCase("2001-01-27", 2001, 01, 27)]
    [TestCase("2010-12-21", 2010, 12, 21)]
    public void ShouldConvertDateToFormatCorrectly(string testDate, int assertYear, int assertMonth, int assertDay)
    {
        var json = Encoding.UTF8.GetBytes($"\"{testDate}\"");
        var reader = new Utf8JsonReader(json.AsSpan());
        
        Assert.IsTrue(reader.Read());
        
        var converter = new DateTimeConverter();
        var result = converter.Read(ref reader, typeof(DateTimeConverter), null!);
        
        Assert.That(result, Is.EqualTo(new DateTime(assertYear, assertMonth, assertDay)));

        var writer = new Utf8JsonWriter(new MemoryStream());
        
        Assert.DoesNotThrow(() =>converter.Write(writer, result, null!));
    }
}