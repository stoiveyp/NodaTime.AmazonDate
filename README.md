# NodaTime.AmazonDate

Converts Alexa AMAZON.DATE slot values into NodaTime Dates

```csharp
var dates = AmazonDateParser.Parse("2015-11");

var from = dates.From //NodaTime LocalDate set to 2015/11/01
var to   = dates.To   //NodaTime LocalDate set to 2015/12/01
```