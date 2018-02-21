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
            Utility.IsFirst2015(result.From);
            Utility.IsFirst2015(result.To);
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
