using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Utf8Json.Tests
{
    public class GenericCustomFormatterTest
    {
        [JsonFormatter(typeof(CustomFormatter<>))]
        public class Custom<T>
        {
            public T MyProperty { get; set; }
        }

        public class Custom2<T>
        {
            [JsonFormatter(typeof(CustomFormatter<>))]
            public T MyProperty { get; set; }
        }


        public class CustomFormatter<T> : IJsonFormatter<T>
        {
            public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
            {
                throw new NotImplementedException();
            }

            public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
            {
                // throws NotImplementedException for OK.
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void Foo()
        {
            AssertEx.Throws<NotImplementedException>(() =>
            {
                JsonSerializer.Serialize(new Custom<int>() { MyProperty = 9999 });
            });

            // TODO:Currently not supported...
            // AssertEx.Throws<NotImplementedException>(() =>
            //{
            //    JsonSerializer.Serialize(new Custom2<int>() { MyProperty = 9999 });
            //}
        }
    }
}
