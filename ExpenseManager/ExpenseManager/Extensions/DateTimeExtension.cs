using System;

namespace ExpenseManager.Extensions
{
    public static class DateTimeExtension
    {
        public static double ToUnixMillis(this DateTime dateTime) =>
            dateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

        public static DateTime ToFirstDayOfMonth(this DateTime dateTime) =>
            new DateTime(dateTime.Year, dateTime.Month, 1);

        public static DateTime ToLastDateOfMonth(this DateTime dateTime) =>
            dateTime.ToFirstDayOfMonth().AddMonths(1).AddMilliseconds(-1);

        public static DateTime ToNextDay(this DateTime dateTime) =>
            dateTime.AddDays(1);

        public static DateTime ToPreviousDay(this DateTime dateTime) =>
            dateTime.AddDays(-1);

        public static DateTime ToStartOfWeek(this DateTime dateTime, DayOfWeek startDayOfWeek) =>
            dateTime.AddDays(-1 * (7 + (dateTime.DayOfWeek - startDayOfWeek)) % 7);

        public static int LastDayOfMonth(this DateTime dateTime) =>
            DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

        //todo if First day of week - Sunday
        public static int NumberOfWeeksInMonth(this DateTime dateTime)
        {
            int daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            DateTime firstOfMonth = dateTime.ToFirstDayOfMonth();

            int firstDayOfMonth = (int) firstOfMonth.DayOfWeek;

            int weeksInMonth = (int) Math.Floor((firstDayOfMonth + daysInMonth) / 7f);
            return weeksInMonth;
        }

        //todo if First day of week - Sunday
        public static int NumberOfWeeksInMonthRows(this DateTime dateTime)
        {
            int daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            DateTime firstOfMonth = dateTime.ToFirstDayOfMonth();

            int firstDayOfMonth = (int) firstOfMonth.DayOfWeek;

            int weeksInMonth = (int) Math.Ceiling((firstDayOfMonth + daysInMonth) / 7f);
            return weeksInMonth;
        }

        public static bool IsLastDayOfMonth(this DateTime dateTime) =>
            DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;

        public static bool IsLastDayOfWeek(this DateTime dateTime, DayOfWeek endDayOfWeek) =>
            dateTime.DayOfWeek == endDayOfWeek;

        public static bool IsFirstDayOfWeek(this DateTime dateTime, DayOfWeek startDayOfWeek) =>
            dateTime.DayOfWeek == startDayOfWeek;

        public static bool IsDateTimeInRange(this DateTime dateTimeToCheck, DateTime startDateTime,
            DateTime endDateTime) =>
            startDateTime <= dateTimeToCheck && dateTimeToCheck <= endDateTime;
    }
}