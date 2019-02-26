using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;

namespace Utf8Json.Tests
{
    public class WithBomTest
    {


        public class MyClass
        {
            public int MyProperty { get; set; }
        }

        [Fact]
        public void Bom()
        {
            var bin = JsonSerializer.Serialize(new MyClass { MyProperty = 100 });
            var withBom = Encoding.UTF8.GetPreamble().Concat(bin).ToArray();
            var hugahuga = JsonSerializer.Deserialize<MyClass>(withBom);

            hugahuga.MyProperty.Is(100);

            JsonSerializer.Deserialize<int>(JsonSerializer.Serialize(1)).Is(1);
            JsonSerializer.Deserialize<int>(JsonSerializer.Serialize(12)).Is(12);
            JsonSerializer.Deserialize<int>(JsonSerializer.Serialize(123)).Is(123);
            JsonSerializer.Deserialize<int>(JsonSerializer.Serialize(1234)).Is(1234);
        }
    }
}
