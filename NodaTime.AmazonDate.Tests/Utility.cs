using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NodaTime.AmazonDate.Tests
{
    public static class Utility
    {
        public static void IsFirst2015(LocalDate date) => IsDate(date, 2015, 1, 1);

        public static void IsDate(LocalDate result, int year, int month, int day)
        {
            Assert.Equal(year, result.Year);
            Assert.Equal(month, result.Month);
            Assert.Equal(day, result.Day);
        }
    }
}
