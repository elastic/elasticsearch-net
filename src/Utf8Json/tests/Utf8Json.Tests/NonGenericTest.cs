using SharedData;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Utf8Json.Resolvers;

namespace Utf8Json.Tests
{
    public class NonGenericTest
    {
        T ConvertNonGeneric<T>(T obj)
        {
            var t = obj.GetType();
            return (T)JsonSerializer.NonGeneric.Deserialize(t, JsonSerializer.NonGeneric.Serialize(t, obj));
        }

        [Fact]
        public void NonGeneric()
        {
            var data = new FirstSimpleData { Prop1 = 9, Prop2 = "hoge", Prop3 = 999 };
            var t = typeof(FirstSimpleData);
            var ms = new MemoryStream();

            var data1 = JsonSerializer.NonGeneric.Deserialize(t, JsonSerializer.NonGeneric.Serialize(t, data)) as FirstSimpleData;
            var data2 = JsonSerializer.NonGeneric.Deserialize(t, JsonSerializer.NonGeneric.Serialize(t, data , StandardResolver.Default)) as FirstSimpleData;

            JsonSerializer.NonGeneric.Serialize(t, ms, data);
            ms.Position = 0;
            var data3 = JsonSerializer.NonGeneric.Deserialize(t, ms) as FirstSimpleData;

            ms = new MemoryStream();
            JsonSerializer.NonGeneric.Serialize(t, ms, data);
            ms.Position = 0;
            var data4 = JsonSerializer.NonGeneric.Deserialize(t, ms, StandardResolver.Default) as FirstSimpleData;

            new[] { data1.Prop1, data2.Prop1, data3.Prop1, data4.Prop1 }.Distinct().Is(data.Prop1);
            new[] { data1.Prop2, data2.Prop2, data3.Prop2, data4.Prop2 }.Distinct().Is(data.Prop2);
            new[] { data1.Prop3, data2.Prop3, data3.Prop3, data4.Prop3 }.Distinct().Is(data.Prop3);
        }

        [Fact]
        public void WriterReader()
        {
            var data = new FirstSimpleData { Prop1 = 9, Prop2 = "hoge", Prop3 = 999 };
            var t = typeof(FirstSimpleData);

            var data1 = JsonSerializer.NonGeneric.Deserialize(t, JsonSerializer.NonGeneric.Serialize(t, data)) as FirstSimpleData;
            var data2 = JsonSerializer.NonGeneric.Deserialize(t, JsonSerializer.NonGeneric.Serialize(t, data, StandardResolver.Default)) as FirstSimpleData;


            var ms = new JsonWriter();
            JsonSerializer.NonGeneric.Serialize(t, ref ms, data);
            var ms2 = new JsonReader(ms.ToUtf8ByteArray());
            var data3 = JsonSerializer.NonGeneric.Deserialize(t, ref ms2) as FirstSimpleData;

            ms = new JsonWriter();
            JsonSerializer.NonGeneric.Serialize(t, ref ms, data, StandardResolver.Default);
            ms2 = new JsonReader(ms.ToUtf8ByteArray());
            var data4 = JsonSerializer.NonGeneric.Deserialize(t, ref ms2, StandardResolver.Default) as FirstSimpleData;

            new[] { data1.Prop1, data2.Prop1, data3.Prop1, data4.Prop1 }.Distinct().Is(data.Prop1);
            new[] { data1.Prop2, data2.Prop2, data3.Prop2, data4.Prop2 }.Distinct().Is(data.Prop2);
            new[] { data1.Prop3, data2.Prop3, data3.Prop3, data4.Prop3 }.Distinct().Is(data.Prop3);
        }
    }
}
