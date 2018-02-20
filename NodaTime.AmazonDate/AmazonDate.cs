using System;
namespace NodaTime.AmazonDate
{
    public class AmazonDate
    {
        public LocalDate From { get; }
        public LocalDate To { get; }

        public AmazonDate(LocalDate from, LocalDate to)
        {
            From = from;
            To = to;
        }
    }
}
