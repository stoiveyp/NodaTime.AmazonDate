using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NodaTime.AmazonDate.Tests
{
    public class WeekNumberTests
    {
        private const int Year = 2015;
        private const int Month = 11;
        private const int FromDay = 23;
        private const int ToDay = 29;

        private const string ValidWeekDate = "2015-W48";
        private const string InvalidWeekDate = "2015-W60";

        [Fact]
        public void ValidWeekReturnsCorrectDates()
        {
            var result = AmazonDateParser.Parse(ValidWeekDate);
            Assert.NotNull(result);
            Utility.IsDate(result.From,Year,Month,FromDay);
            Utility.IsDate(result.To,Year,Month,ToDay);
        }

        [Fact]
        public void InvalidWeekNumberReturnsNull()
        {
            var result = AmazonDateParser.Parse(InvalidWeekDate);
            Assert.Null(result);
        }
    }
}
