using System;
using System.Globalization;
using Xunit;

namespace NodaTime.AmazonDate.Tests
{
    public class LocalDateTimeTests
    {
        private const string LocalDateString = "2015-01-01";
        private const string InvalidDateString = "abcd";

        [Fact]
        public void ParseValidLocalDate()
        {
            var result = AmazonDateParser.Parse(LocalDateString);
            IsFirst2015(result.From);
            IsFirst2015(result.To);
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
            AmazonDate testResultDate;
            var result = AmazonDateParser.TryParse(LocalDateString, out testResultDate);
            Assert.True(result);
            IsFirst2015(testResultDate.From);
            IsFirst2015(testResultDate.To);
        }

        [Fact]
        public void TryParseInvalidLocalDate()
        {
            AmazonDate testResultDate;
            var result = AmazonDateParser.TryParse(InvalidDateString, out testResultDate);
            Assert.False(result);
            Assert.Null(testResultDate);
        }

        private void IsFirst2015(LocalDate date) => IsDate(date, 2015, 1, 1);

        private void IsDate(LocalDate result, int year, int month, int day)
        {
            Assert.Equal(year, result.Year);
            Assert.Equal(month,result.Month);
            Assert.Equal(day, result.Day);
        }
    }
}
