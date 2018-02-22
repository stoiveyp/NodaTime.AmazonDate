using System;
using System.Globalization;
using Xunit;

namespace NodaTime.AmazonDate.Tests
{
    public class LocalDateTimeTests
    {
        private const string LocalDateString = "2015-01-01";
        private const string InvalidDateString = "abcd";
        private const string MonthString = "2015-11";
        private const string YearString = "2015";
        private const string DecadeString = "201X";

        [Fact]
        public void ParseValidLocalDate()
        {
            var result = AmazonDateParser.Parse(LocalDateString);
            Utility.IsFirst2015(result.From);
            Utility.IsFirst2015(result.To);
        }

        [Fact]
        public void ParseMonthOnlyDate()
        {
            var result = AmazonDateParser.Parse(MonthString);
            Utility.IsDate(result.From,2015,11,01);
            Utility.IsDate(result.To,2015,12,01);
        }

        [Fact]
        public void ParseYearOnlyDate()
        {
            var result = AmazonDateParser.Parse(YearString);
            Utility.IsDate(result.From, 2015, 01, 01);
            Utility.IsDate(result.To, 2016, 01, 01);
        }

        [Fact]
        public void ParseDecadeOnlyDate()
        {
            var result = AmazonDateParser.Parse(DecadeString);
            Utility.IsDate(result.From, 2010, 01, 01);
            Utility.IsDate(result.To, 2020, 01, 01);
        }

        [Fact]
        public void ParseInvalidLocalDate()
        {
            var result = AmazonDateParser.Parse(InvalidDateString);
            Assert.Null(result);
        }

        [Fact]
        public void TryParseValidLocalDate()
        {
            var result = AmazonDateParser.TryParse(LocalDateString, out var testResultDate);
            Assert.True(result);
            Utility.IsFirst2015(testResultDate.From);
            Utility.IsFirst2015(testResultDate.To);
        }

        [Fact]
        public void TryParseInvalidLocalDate()
        {
            var result = AmazonDateParser.TryParse(InvalidDateString, out var testResultDate);
            Assert.False(result);
            Assert.Null(testResultDate);
        }
    }
}
