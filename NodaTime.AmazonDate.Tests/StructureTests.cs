using System;
using Xunit;

namespace NodaTime.AmazonDate.Tests
{
    public class StructureTests
    {
        [Fact]
        public void EnsurePropertiesFit()
        {
            var from = new LocalDate(2015, 01, 01);
            var to = new LocalDate(2016, 01, 01);
            var date = new AmazonDate(from, to);
            Assert.Equal(from, date.From);
            Assert.Equal(to, date.To);
        }
    }
}
