using System;
using System.Text.RegularExpressions;

namespace NodaTime.AmazonDate
{
    public static class AmazonDateParser
    {
        private static readonly Regex WeekNumberParser = new Regex(@"(?<year>\d{4})-W(?<weekNumber>\d\d)(?<weekend>-WE)?", RegexOptions.Compiled);

        public static AmazonDate Parse(string value)
        {
            //NodaTime.AnnualDate
            //NodaTime.Calendars.WeekYearRules
            var local = Text.LocalDatePattern.Iso.Parse(value);
            if (local.Success)
            {
                return new AmazonDate(local.Value, local.Value);
            }

            var weekNumberMatch = WeekNumberParser.Match(value);
            if (weekNumberMatch.Success)
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
