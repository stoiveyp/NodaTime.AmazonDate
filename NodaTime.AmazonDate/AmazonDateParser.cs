using System;
using System.Globalization;

namespace NodaTime.AmazonDate
{
    public static class AmazonDateParser
    {
        public static AmazonDate Parse(string value)
        {
            //NodaTime.AnnualDate
            //NodaTime.Calendars.WeekYearRules
            var local = Text.LocalDatePattern.Iso.Parse(value);
            if(!local.Success)
            {
                return null;
            }
            return new AmazonDate(local.Value,local.Value);
        }

        public static bool TryParse(string value, out AmazonDate date)
        {
            try{
                var resolution = Parse(value);
                date = resolution;
                return resolution != null;
            }
            catch(Exception)
            {
                date = null;
                return false;
            }
        }
    }
}
