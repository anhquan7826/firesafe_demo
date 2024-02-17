using System.Globalization;

namespace Application.Utils;

public static class DateTimeExtension
{
    public static string ToStringUtc(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
    }

    public static DateTime ParseUtc(string timestamp)
    {
        return DateTime.Parse(timestamp, CultureInfo.InvariantCulture).ToUniversalTime();
    }
}