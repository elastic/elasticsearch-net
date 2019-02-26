using System;
using System.Collections.Generic;
using System.Text;
using Utf8Json.Formatters;
using Utf8Json.Resolvers;
using Xunit;

namespace Utf8Json.Tests
{
    public class CompositeResolverTest
    {
        class TestFormatter : IJsonFormatter<MyClass>
        {
            public MyClass Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
            {
                var i = reader.ReadInt32();
                return new MyClass { MyProperty = i * 2 };
            }

            public void Serialize(ref JsonWriter writer, MyClass value, IJsonFormatterResolver formatterResolver)
            {
                writer.WriteInt32(value.MyProperty * 2);
            }
        }

        public class MyClass
        {
            public int MyProperty { get; set; }
        }

        [Fact]
        public void Composite()
        {
            var emptyResolver = CompositeResolver.Create(new IJsonFormatter[0], new IJsonFormatterResolver[0]);
            AssertEx.Catch<FormatterNotRegisteredException>(() => emptyResolver.GetFormatterWithVerify<int>());

            var resolver = CompositeResolver.Create(new[] { new TestFormatter() }, new[] { BuiltinResolver.Instance });
            resolver.GetFormatterWithVerify<int>().IsInstanceOf<Int32Formatter>();

            var json = JsonSerializer.ToJsonString(new MyClass { MyProperty = 3 }, resolver);
            json.Is("6");
            JsonSerializer.Deserialize<MyClass>(json, resolver).MyProperty.Is(12);
        }
    }
}
