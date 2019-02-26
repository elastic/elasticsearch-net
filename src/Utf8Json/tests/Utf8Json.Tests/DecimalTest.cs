using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class DecimalTest
    {
        public class Foo
        {
            public decimal Bar { get; set; }
            public string More { get; set; }
        }

        [Fact]
        public void D()
        {
            var d = decimal.MaxValue;
            var bin = JsonSerializer.Serialize(d);
            JsonSerializer.Deserialize<decimal>(bin).Is(d);

            JsonSerializer.Deserialize<decimal>("79228162514264337593543950335.0").Is(decimal.MaxValue);
            JsonSerializer.Deserialize<decimal>("\"79228162514264337593543950335.0\"").Is(decimal.MaxValue);

            var foo = JsonSerializer.Serialize(new Foo { Bar = -31.42323m, More = "mmmm" });
            var ddd = JsonSerializer.Deserialize<Foo>(foo);
            ddd.Bar.Is(-31.42323m);
            ddd.More.Is("mmmm");

            JsonSerializer.Deserialize<Foo>("{\"Bar\":1.23}").Bar.Is(1.23m);
        }
    }
}
