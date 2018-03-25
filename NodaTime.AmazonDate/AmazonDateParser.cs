using System;
using System.Text.RegularExpressions;

namespace NodaTime.AmazonDate
{
    public static class AmazonDateParser
    {
        private static readonly Regex WeekNumberParser = new Regex(@"(?<year>\d{4})-W(?<weekNumber>\d\d)(?<weekend>-WE)?", RegexOptions.Compiled);

        public static AmazonDate Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                return null;
            }

            for (var pos = 0; pos <= 2; pos++)
            {
                if (!char.IsNumber(value[pos]))
                {
                    return null;
                }
            }

            if (value.Length == 4)
            {
                return YearParse(value);
            }

            var monthRange = MonthParse(value);
            if(monthRange != null)
            {
                return monthRange;
            }

            if (value.Length < 8)
            {
                return null;
            }

            var local = Text.LocalDatePattern.Iso.Parse(value);
            if (local.Success)
            {
                return new AmazonDate(local.Value, local.Value);
            }

            var weekNumberMatch = WeekNumberParser.Match(value);
            if (weekNumberMatch.Success)
            {
                return WeekNumberParse(weekNumberMatch);
            }

            return null;
        }

        private static AmazonDate WeekNumberParse(Match weekNumberMatch)
        {
            var year = int.Parse(weekNumberMatch.Groups["year"].Value);
                var weekNumber = int.Parse(weekNumberMatch.Groups["weekNumber"].Value);
                if (weekNumber > 53)
                {
                    return null;
                }

                var fromDay = weekNumberMatch.Groups["weekend"].Length > 0 ? IsoDayOfWeek.Saturday : IsoDayOfWeek.Monday;

                var from = Calendars.WeekYearRules.Iso.GetLocalDate(year, weekNumber, fromDay, CalendarSystem.Iso);
                var to = Calendars.WeekYearRules.Iso.GetLocalDate(year, weekNumber, IsoDayOfWeek.Sunday, CalendarSystem.Iso);
                return new AmazonDate(from, to);
        }

        private static AmazonDate MonthParse(string value)
        {
            if (value.Length == 7 &&  
            char.IsNumber(value[3]) &&
            value[4] == '-' &&
            char.IsNumber(value[5]) &&
            char.IsNumber(value[6]))
            {
                var year = int.Parse(value.Substring(0, 4));
                var month = int.Parse(value.Substring(5, 2));
                var from = new LocalDate(year, month, 01);
                return new AmazonDate(from, from.PlusMonths(1));
            }
            return null;
        }

        private static AmazonDate YearParse(string value)
        {
            if (value[3] == 'X')
                {
                    var year = int.Parse(value.Substring(0, 3) + "0");
                    var from = new LocalDate(year, 01, 01);
                    return new AmazonDate(from, from.PlusYears(10));
                }

                if (char.IsNumber(value[3]))
                {
                    var year = int.Parse(value);
                    var from = new LocalDate(year, 01, 01);
                    return new AmazonDate(from, from.PlusYears(1));
                }
                return null;
        }

        public static bool TryParse(string value, out AmazonDate date)
        {
            try
            {
                var resolution = Parse(value);
                date = resolution;
                return resolution != null;
            }
            catch (Exception)
            {
                date = null;
                return false;
            }
        }
    }
}
