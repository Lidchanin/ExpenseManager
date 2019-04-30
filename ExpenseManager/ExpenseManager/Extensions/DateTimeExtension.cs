using System;

namespace ExpenseManager.Extensions
{
    public static class DateTimeExtension
    {
        public static double ToUnixMillis(this DateTime dateTime) =>
            dateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }
}